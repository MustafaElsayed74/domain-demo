-- ============================================================================
-- Travel Marketplace - Database Constraints, Triggers & Procedures
-- PostgreSQL Migration Script
-- Generated: 2026-02-05
-- ============================================================================

-- ============================================================================
-- 1. CATEGORY PROTECTION TRIGGERS
-- Prevent INSERT/DELETE on categories table (only 4 fixed categories allowed)
-- ============================================================================

CREATE OR REPLACE FUNCTION prevent_category_changes()
RETURNS TRIGGER AS $$
BEGIN
  IF TG_OP = 'INSERT' THEN
    RAISE EXCEPTION 'Cannot add new categories - only 4 fixed categories allowed';
  ELSIF TG_OP = 'DELETE' THEN
    RAISE EXCEPTION 'Cannot delete categories - only 4 fixed categories allowed';
  END IF;
  RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_prevent_category_insert
  BEFORE INSERT ON categories
  FOR EACH ROW EXECUTE FUNCTION prevent_category_changes();

CREATE TRIGGER trg_prevent_category_delete
  BEFORE DELETE ON categories
  FOR EACH ROW EXECUTE FUNCTION prevent_category_changes();


-- ============================================================================
-- 2. BOOKING REFERENCE GENERATOR
-- Auto-generate booking reference on INSERT: {PREFIX}-{YEAR}-{SEQUENCE}
-- ============================================================================

CREATE OR REPLACE FUNCTION generate_booking_reference()
RETURNS TRIGGER AS $$
DECLARE
  prefix VARCHAR(3);
  year_str VARCHAR(4);
  sequence_num INT;
BEGIN
  -- Determine prefix based on category
  prefix := CASE NEW.category
    WHEN 0 THEN 'TOR'  -- Tour
    WHEN 1 THEN 'FLT'  -- Flight
    WHEN 2 THEN 'CAR'  -- Car
    WHEN 3 THEN 'HTL'  -- Hotel
    ELSE 'TRV'
  END;
  
  year_str := EXTRACT(YEAR FROM CURRENT_DATE)::VARCHAR;
  
  -- Get next sequence for this category+year
  SELECT COALESCE(MAX(CAST(SUBSTRING(booking_reference FROM 13) AS INT)), 0) + 1
  INTO sequence_num
  FROM bookings
  WHERE category = NEW.category 
    AND EXTRACT(YEAR FROM created_at) = EXTRACT(YEAR FROM CURRENT_DATE);
  
  NEW.booking_reference := prefix || '-' || year_str || '-' || LPAD(sequence_num::VARCHAR, 7, '0');
  
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_generate_booking_reference
  BEFORE INSERT ON bookings
  FOR EACH ROW 
  WHEN (NEW.booking_reference IS NULL OR NEW.booking_reference = '')
  EXECUTE FUNCTION generate_booking_reference();


-- ============================================================================
-- 3. REVIEW RATING AGGREGATION TRIGGER
-- Update product avg_rating and review_count when review changes
-- ============================================================================

CREATE OR REPLACE FUNCTION update_product_ratings()
RETURNS TRIGGER AS $$
DECLARE
  target_table VARCHAR(50);
  item_id_val BIGINT;
  category_val INT;
BEGIN
  -- Handle both INSERT/UPDATE and DELETE
  IF TG_OP = 'DELETE' THEN
    category_val := OLD.category;
    item_id_val := OLD.item_id;
  ELSE
    category_val := NEW.category;
    item_id_val := NEW.item_id;
  END IF;

  -- Determine target table based on category
  target_table := CASE category_val
    WHEN 0 THEN 'tours'    -- Tour
    WHEN 1 THEN 'flights'  -- Flight
    WHEN 2 THEN 'cars'     -- Car
    WHEN 3 THEN 'hotels'   -- Hotel
    WHEN 4 THEN 'rooms'    -- Room
  END;
  
  -- Update avg_rating and review_count
  EXECUTE format('
    UPDATE %I SET
      avg_rating = (
        SELECT ROUND(AVG(rating)::NUMERIC, 2)
        FROM reviews
        WHERE category = $1 AND item_id = $2 AND status = 1
      ),
      review_count = (
        SELECT COUNT(*)
        FROM reviews
        WHERE category = $1 AND item_id = $2 AND status = 1
      ),
      updated_at = NOW()
    WHERE id = $2
  ', target_table) USING category_val, item_id_val;
  
  IF TG_OP = 'DELETE' THEN
    RETURN OLD;
  ELSE
    RETURN NEW;
  END IF;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_ratings_after_insert
  AFTER INSERT ON reviews
  FOR EACH ROW 
  WHEN (NEW.status = 1)  -- 1 = Approved
  EXECUTE FUNCTION update_product_ratings();

CREATE TRIGGER trg_update_ratings_after_update
  AFTER UPDATE ON reviews
  FOR EACH ROW
  WHEN (OLD.status IS DISTINCT FROM NEW.status OR OLD.rating IS DISTINCT FROM NEW.rating)
  EXECUTE FUNCTION update_product_ratings();

CREATE TRIGGER trg_update_ratings_after_delete
  AFTER DELETE ON reviews
  FOR EACH ROW
  WHEN (OLD.status = 1)  -- 1 = Approved
  EXECUTE FUNCTION update_product_ratings();


-- ============================================================================
-- 4. TOUR AVAILABILITY DECREMENT TRIGGER
-- Decrement tour schedule availability on booking confirmation
-- ============================================================================

CREATE OR REPLACE FUNCTION decrement_tour_availability()
RETURNS TRIGGER AS $$
BEGIN
  IF NEW.category = 0 AND NEW.status = 1 THEN  -- Tour, Confirmed
    UPDATE tour_schedules
    SET available_slots = available_slots - COALESCE(
          (SELECT COALESCE(adults, 0) + COALESCE(children, 0) 
           FROM booking_details WHERE booking_id = NEW.id), 1),
        status = CASE WHEN available_slots - 1 <= 0 THEN 1 ELSE status END,  -- 1 = Full
        updated_at = NOW()
    WHERE id = (SELECT tour_schedule_id FROM booking_details WHERE booking_id = NEW.id)
      AND available_slots > 0;
    
    IF NOT FOUND THEN
      RAISE EXCEPTION 'No available slots for this tour';
    END IF;
  END IF;
  
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_decrement_tour_availability
  AFTER UPDATE ON bookings
  FOR EACH ROW
  WHEN (NEW.category = 0 AND NEW.status = 1 AND OLD.status != 1)
  EXECUTE FUNCTION decrement_tour_availability();


-- ============================================================================
-- 5. COUPON USAGE COUNTER TRIGGER
-- Increment coupon used_count when coupon is used
-- ============================================================================

CREATE OR REPLACE FUNCTION increment_coupon_usage()
RETURNS TRIGGER AS $$
BEGIN
  UPDATE coupons
  SET used_count = used_count + 1,
      updated_at = NOW()
  WHERE id = NEW.coupon_id;
  
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_increment_coupon_usage
  AFTER INSERT ON coupon_usages
  FOR EACH ROW
  EXECUTE FUNCTION increment_coupon_usage();


-- ============================================================================
-- 6. AUDIT LOG TRIGGER
-- Auto-log all changes to audited tables
-- ============================================================================

CREATE OR REPLACE FUNCTION log_audit()
RETURNS TRIGGER AS $$
BEGIN
  IF TG_OP = 'INSERT' THEN
    INSERT INTO audit_logs (user_id, action, entity_type, entity_id, new_values_json, ip_address, created_at)
    VALUES (
      NULLIF(current_setting('app.current_user_id', true), '')::BIGINT,
      'CREATE_' || UPPER(TG_TABLE_NAME),
      TG_TABLE_NAME,
      NEW.id,
      row_to_json(NEW)::TEXT,
      current_setting('app.client_ip', true),
      NOW()
    );
  ELSIF TG_OP = 'UPDATE' THEN
    INSERT INTO audit_logs (user_id, action, entity_type, entity_id, old_values_json, new_values_json, ip_address, created_at)
    VALUES (
      NULLIF(current_setting('app.current_user_id', true), '')::BIGINT,
      'UPDATE_' || UPPER(TG_TABLE_NAME),
      TG_TABLE_NAME,
      NEW.id,
      row_to_json(OLD)::TEXT,
      row_to_json(NEW)::TEXT,
      current_setting('app.client_ip', true),
      NOW()
    );
  ELSIF TG_OP = 'DELETE' THEN
    INSERT INTO audit_logs (user_id, action, entity_type, entity_id, old_values_json, ip_address, created_at)
    VALUES (
      NULLIF(current_setting('app.current_user_id', true), '')::BIGINT,
      'DELETE_' || UPPER(TG_TABLE_NAME),
      TG_TABLE_NAME,
      OLD.id,
      row_to_json(OLD)::TEXT,
      current_setting('app.client_ip', true),
      NOW()
    );
  END IF;
  
  RETURN NULL;
END;
$$ LANGUAGE plpgsql;

-- Apply audit trigger to key tables
CREATE TRIGGER trg_audit_tours 
  AFTER INSERT OR UPDATE OR DELETE ON tours
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_flights 
  AFTER INSERT OR UPDATE OR DELETE ON flights
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_cars 
  AFTER INSERT OR UPDATE OR DELETE ON cars
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_hotels 
  AFTER INSERT OR UPDATE OR DELETE ON hotels
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_rooms 
  AFTER INSERT OR UPDATE OR DELETE ON rooms
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_bookings 
  AFTER INSERT OR UPDATE OR DELETE ON bookings
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_payments 
  AFTER INSERT OR UPDATE OR DELETE ON payments
  FOR EACH ROW EXECUTE FUNCTION log_audit();

CREATE TRIGGER trg_audit_users 
  AFTER INSERT OR UPDATE OR DELETE ON users
  FOR EACH ROW EXECUTE FUNCTION log_audit();


-- ============================================================================
-- 7. CHECK CONSTRAINTS
-- ============================================================================

-- Rating must be 1-5
ALTER TABLE reviews ADD CONSTRAINT chk_rating_range 
  CHECK (rating >= 1 AND rating <= 5);

-- Positive prices for tour tiers
ALTER TABLE tour_price_tiers ADD CONSTRAINT chk_tour_positive_prices 
  CHECK (adult_price >= 0 AND child_price >= 0 AND infant_price >= 0);

-- Positive prices for flight fares
ALTER TABLE flight_fares ADD CONSTRAINT chk_flight_positive_fare 
  CHECK (base_price >= 0 AND taxes >= 0 AND fees >= 0);

-- Positive car specifications
ALTER TABLE cars ADD CONSTRAINT chk_car_positive_specs 
  CHECK (seats_count > 0 AND doors_count > 0);

-- Positive room occupancy and pricing
ALTER TABLE rooms ADD CONSTRAINT chk_room_positive_occupancy 
  CHECK (max_adults > 0 AND price_per_night >= 0);

-- Flight arrival must be after departure
ALTER TABLE flights ADD CONSTRAINT chk_flight_times 
  CHECK (arrival_at > departure_at);

-- Tour schedule end date must be >= start date
ALTER TABLE tour_schedules ADD CONSTRAINT chk_schedule_dates 
  CHECK (end_date >= start_date);

-- Booking checkout must be >= checkin
ALTER TABLE bookings ADD CONSTRAINT chk_booking_dates 
  CHECK (check_out_date IS NULL OR check_out_date >= check_in_date);

-- Coupon validity dates
ALTER TABLE coupons ADD CONSTRAINT chk_coupon_dates 
  CHECK (valid_until >= valid_from);

-- Tour schedule capacity
ALTER TABLE tour_schedules ADD CONSTRAINT chk_tour_available_slots 
  CHECK (available_slots >= 0 AND available_slots <= total_capacity);

-- Flight fare seats
ALTER TABLE flight_fares ADD CONSTRAINT chk_flight_seats_available 
  CHECK (seats_available >= 0);

-- Booking amounts must be non-negative
ALTER TABLE bookings ADD CONSTRAINT chk_booking_positive_amounts 
  CHECK (subtotal >= 0 AND taxes >= 0 AND fees >= 0 AND discount >= 0 AND total_price >= 0);

-- Payment amount must be positive
ALTER TABLE payments ADD CONSTRAINT chk_payment_positive_amount 
  CHECK (amount > 0);

-- Refund cannot exceed payment amount
ALTER TABLE payments ADD CONSTRAINT chk_refund_amount 
  CHECK (refund_amount IS NULL OR refund_amount <= amount);

-- Coupon discount value must be positive
ALTER TABLE coupons ADD CONSTRAINT chk_coupon_positive_discount 
  CHECK (discount_value > 0);

-- Coupon usage limit >= used count
ALTER TABLE coupons ADD CONSTRAINT chk_coupon_usage_limit 
  CHECK (usage_limit IS NULL OR usage_limit >= used_count);

-- Star rating must be 0-5
ALTER TABLE hotels ADD CONSTRAINT chk_hotel_star_rating 
  CHECK (star_rating IS NULL OR (star_rating >= 0 AND star_rating <= 5));

-- Review helpful votes non-negative
ALTER TABLE reviews ADD CONSTRAINT chk_review_helpful_votes 
  CHECK (helpful_votes >= 0);


-- ============================================================================
-- 8. UNIQUE CONSTRAINTS (in addition to entity-level ones)
-- ============================================================================

-- One review per user per item
CREATE UNIQUE INDEX uix_reviews_user_item 
  ON reviews (user_id, category, item_id) 
  WHERE status != 2;  -- 2 = Rejected

-- One coupon per booking
CREATE UNIQUE INDEX uix_coupon_usage_booking 
  ON coupon_usages (booking_id);

-- Unique favorite per user per item
CREATE UNIQUE INDEX uix_favorites_user_item 
  ON favorites (user_id, category, item_id);

-- Unique transaction ID per payment
CREATE UNIQUE INDEX uix_payment_transaction 
  ON payments (transaction_id) 
  WHERE transaction_id IS NOT NULL;


-- ============================================================================
-- 9. INDEXES FOR PERFORMANCE
-- ============================================================================

-- Booking indexes
CREATE INDEX ix_bookings_user_status ON bookings (user_id, status);
CREATE INDEX ix_bookings_category_item ON bookings (category, item_id);
CREATE INDEX ix_bookings_dates ON bookings (check_in_date, check_out_date);
CREATE INDEX ix_bookings_booking_date ON bookings (booking_date);

-- Review indexes
CREATE INDEX ix_reviews_category_item_status ON reviews (category, item_id, status);
CREATE INDEX ix_reviews_user ON reviews (user_id);
CREATE INDEX ix_reviews_status ON reviews (status);

-- Favorite indexes
CREATE INDEX ix_favorites_user_category ON favorites (user_id, category);
CREATE INDEX ix_favorites_category_item ON favorites (category, item_id);

-- Coupon indexes
CREATE INDEX ix_coupons_active_dates ON coupons (active, valid_from, valid_until);

-- Audit log indexes
CREATE INDEX ix_audit_logs_user_created ON audit_logs (user_id, created_at);
CREATE INDEX ix_audit_logs_entity ON audit_logs (entity_type, entity_id);
CREATE INDEX ix_audit_logs_action ON audit_logs (action);
CREATE INDEX ix_audit_logs_created ON audit_logs (created_at);

-- Payment indexes
CREATE INDEX ix_payments_booking ON payments (booking_id);
CREATE INDEX ix_payments_status ON payments (status);
CREATE INDEX ix_payments_paid_at ON payments (paid_at);


-- ============================================================================
-- 10. HELPER FUNCTIONS
-- ============================================================================

-- Function to set current user for audit context
CREATE OR REPLACE FUNCTION set_audit_context(user_id BIGINT, client_ip VARCHAR(45))
RETURNS VOID AS $$
BEGIN
  PERFORM set_config('app.current_user_id', user_id::TEXT, true);
  PERFORM set_config('app.client_ip', client_ip, true);
END;
$$ LANGUAGE plpgsql;

-- Function to clear audit context
CREATE OR REPLACE FUNCTION clear_audit_context()
RETURNS VOID AS $$
BEGIN
  PERFORM set_config('app.current_user_id', '', true);
  PERFORM set_config('app.client_ip', '', true);
END;
$$ LANGUAGE plpgsql;
