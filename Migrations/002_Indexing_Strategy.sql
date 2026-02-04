-- ============================================================================
-- Travel Marketplace - Indexing Strategy & Performance Optimization
-- PostgreSQL Migration Script
-- Generated: 2026-02-05
-- ============================================================================

-- ============================================================================
-- 1. SEARCH & FILTER INDEXES
-- ============================================================================

-- -----------------------------------------------------------------------------
-- Tours Search Indexes
-- -----------------------------------------------------------------------------

-- Full-text search on tour content
CREATE INDEX idx_tours_search ON tours 
  USING GIN (to_tsvector('english', title || ' ' || summary || ' ' || COALESCE(full_description, '')));

-- Location and rating based filtering
CREATE INDEX idx_tours_location_rating ON tours (location, recommended, avg_rating DESC);

-- Tags search using GIN for JSON array
CREATE INDEX idx_tours_tags ON tours USING GIN (tags_json jsonb_path_ops);

-- Duration filtering
CREATE INDEX idx_tours_duration ON tours (duration_days, difficulty);

-- Price range queries
CREATE INDEX idx_tour_prices ON tour_price_tiers (tour_id, adult_price);


-- -----------------------------------------------------------------------------
-- Flights Search Indexes
-- -----------------------------------------------------------------------------

-- Route and date search (most common query)
CREATE INDEX idx_flights_route_date ON flights (origin_airport_id, destination_airport_id, departure_at);

-- Carrier schedule lookup
CREATE INDEX idx_flights_carrier_date ON flights (carrier_id, departure_at);

-- Active flights only (exclude completed)
CREATE INDEX idx_flights_status ON flights (status) WHERE status != 3;  -- 3 = Completed

-- Airport lookups
CREATE INDEX idx_airports_iata ON airports (iata_code);
CREATE INDEX idx_airports_city ON airports (city, country);

-- Carrier lookup
CREATE INDEX idx_carriers_iata ON carriers (iata_code);


-- -----------------------------------------------------------------------------
-- Cars Search Indexes
-- -----------------------------------------------------------------------------

-- Location and category filtering
CREATE INDEX idx_cars_location_category ON cars (location_id, category, fuel_type);

-- Brand and specs search
CREATE INDEX idx_cars_search ON cars (brand_id, transmission_type, seats_count);

-- Price range
CREATE INDEX idx_car_pricing ON car_pricing_tiers (car_id, hourly_rate);


-- -----------------------------------------------------------------------------
-- Hotels Search Indexes
-- -----------------------------------------------------------------------------

-- City and rating based search
CREATE INDEX idx_hotels_city_rating ON hotels (city, star_rating DESC, avg_rating DESC);

-- Full-text search on hotel content
CREATE INDEX idx_hotels_search ON hotels 
  USING GIN (to_tsvector('english', name || ' ' || COALESCE(description, '')));

-- Geospatial index for nearby search
CREATE INDEX idx_hotels_geo ON hotels (latitude, longitude) 
  WHERE latitude IS NOT NULL AND longitude IS NOT NULL;

-- Country filtering
CREATE INDEX idx_hotels_country ON hotels (country, city);


-- -----------------------------------------------------------------------------
-- Rooms Search Indexes
-- -----------------------------------------------------------------------------

-- Hotel rooms by price
CREATE INDEX idx_rooms_hotel_price ON rooms (hotel_id, price_per_night);

-- Occupancy filtering
CREATE INDEX idx_rooms_occupancy ON rooms (max_adults, max_children);

-- Refundable filter
CREATE INDEX idx_rooms_refundable ON rooms (hotel_id, refundable);


-- ============================================================================
-- 2. BOOKING & TRANSACTION INDEXES
-- ============================================================================

-- User booking history (most common query)
CREATE INDEX idx_bookings_user_date ON bookings (user_id, booking_date DESC);

-- Category and status filtering
CREATE INDEX idx_bookings_category_status ON bookings (category, status, created_at DESC);

-- Pending payments (for follow-up jobs)
CREATE INDEX idx_bookings_payment ON bookings (payment_status) WHERE payment_status = 0;  -- 0 = Pending

-- Upcoming check-ins
CREATE INDEX idx_bookings_checkin ON bookings (check_in_date) 
  WHERE status IN (0, 1);  -- 0 = Pending, 1 = Confirmed

-- Booking reference lookup
CREATE INDEX idx_bookings_reference ON bookings (booking_reference);


-- -----------------------------------------------------------------------------
-- Payments Indexes
-- -----------------------------------------------------------------------------

-- Booking payments
CREATE INDEX idx_payments_booking ON payments (booking_id, status);

-- Gateway analytics
CREATE INDEX idx_payments_gateway_date ON payments (gateway, created_at DESC);

-- Failed payments for retry
CREATE INDEX idx_payments_failed ON payments (status, created_at) WHERE status = 2;  -- 2 = Failed

-- Refund tracking
CREATE INDEX idx_payments_refunds ON payments (refunded_at) WHERE refunded_at IS NOT NULL;


-- -----------------------------------------------------------------------------
-- Reviews Indexes
-- -----------------------------------------------------------------------------

-- Approved reviews for display
CREATE INDEX idx_reviews_category_item_approved ON reviews (category, item_id, status) 
  WHERE status = 1;  -- 1 = Approved

-- Pending moderation queue
CREATE INDEX idx_reviews_pending ON reviews (status, created_at) WHERE status = 0;  -- 0 = Pending

-- User's reviews
CREATE INDEX idx_reviews_user ON reviews (user_id, created_at DESC);


-- -----------------------------------------------------------------------------
-- Audit Logs Indexes (Time-Series)
-- -----------------------------------------------------------------------------

-- Time-based queries
CREATE INDEX idx_audit_time ON audit_logs (created_at DESC);

-- Entity history
CREATE INDEX idx_audit_entity ON audit_logs (entity_type, entity_id, created_at DESC);

-- User activity
CREATE INDEX idx_audit_user ON audit_logs (user_id, created_at DESC) WHERE user_id IS NOT NULL;

-- Action filtering
CREATE INDEX idx_audit_action ON audit_logs (action);


-- ============================================================================
-- 3. PARTIAL INDEXES (For Specific Query Patterns)
-- ============================================================================

-- Active coupons only
CREATE INDEX idx_coupons_active ON coupons (code, valid_from, valid_until) 
  WHERE active = true;

-- Available flight seats only
CREATE INDEX idx_flight_seats_available ON flight_seats (flight_id, cabin_class) 
  WHERE is_available = true;

-- Verified active users
CREATE INDEX idx_users_verified ON users (email) 
  WHERE is_verified = true AND status = 0;  -- 0 = Active

-- Active tours
CREATE INDEX idx_tours_active ON tours (slug) WHERE is_active = true;

-- Active hotels
CREATE INDEX idx_hotels_active ON hotels (slug) WHERE is_active = true;

-- Active rooms
CREATE INDEX idx_rooms_active ON rooms (hotel_id) WHERE is_active = true;

-- Recent bookings (hot data - last 90 days)
CREATE INDEX idx_bookings_recent ON bookings (user_id, created_at DESC) 
  WHERE created_at > NOW() - INTERVAL '90 days';

-- Unlocked users
CREATE INDEX idx_users_unlocked ON users (email) 
  WHERE lockout_end IS NULL OR lockout_end < NOW();


-- ============================================================================
-- 4. COVERING INDEXES (Include Columns to Avoid Table Lookups)
-- ============================================================================

-- Booking list with essential info
CREATE INDEX idx_bookings_list_cover ON bookings (user_id, created_at DESC) 
  INCLUDE (booking_reference, category, status, total_price, currency);

-- Tour listing with display data
CREATE INDEX idx_tours_list_cover ON tours (location, recommended) 
  INCLUDE (title, slug, main_image, avg_rating, review_count, duration_days);

-- Hotel listing with display data
CREATE INDEX idx_hotels_list_cover ON hotels (city, star_rating DESC) 
  INCLUDE (name, slug, main_image, avg_rating, review_count);

-- Flight listing with essential info
CREATE INDEX idx_flights_list_cover ON flights (origin_airport_id, destination_airport_id, departure_at) 
  INCLUDE (flight_number, carrier_id, arrival_at, status);

-- Room listing with pricing
CREATE INDEX idx_rooms_list_cover ON rooms (hotel_id, price_per_night) 
  INCLUDE (name, max_adults, max_children, bed_type, refundable);


-- ============================================================================
-- 5. COMPOSITE INDEXES FOR COMMON JOIN PATTERNS
-- ============================================================================

-- Tour schedules for availability check
CREATE INDEX idx_tour_schedules_availability ON tour_schedules (tour_id, start_date, status) 
  WHERE status = 0;  -- 0 = Active

-- Flight fares by class
CREATE INDEX idx_flight_fares_class ON flight_fares (flight_id, cabin_class, seats_available) 
  WHERE seats_available > 0;

-- Booking details lookup
CREATE INDEX idx_booking_details_booking ON booking_details (booking_id);

-- Room images for gallery
CREATE INDEX idx_room_images_room ON room_images (room_id, sort_order);

-- Tour images for gallery
CREATE INDEX idx_tour_images_tour ON tour_images (tour_id, sort_order);

-- Car images for gallery
CREATE INDEX idx_car_images_car ON car_images (car_id, sort_order);


-- ============================================================================
-- 6. TABLE PARTITIONING STRATEGY
-- ============================================================================

-- Note: Partitioning requires table to be created as partitioned from the start.
-- These are example partition definitions for new installations.

-- Partition bookings by year (for archival and query performance)
-- CREATE TABLE bookings (
--   ... columns ...
-- ) PARTITION BY RANGE (created_at);

-- CREATE TABLE bookings_2025 PARTITION OF bookings 
--   FOR VALUES FROM ('2025-01-01') TO ('2026-01-01');
-- CREATE TABLE bookings_2026 PARTITION OF bookings 
--   FOR VALUES FROM ('2026-01-01') TO ('2027-01-01');
-- CREATE TABLE bookings_2027 PARTITION OF bookings 
--   FOR VALUES FROM ('2027-01-01') TO ('2028-01-01');

-- Partition audit_logs by month (high volume, time-series data)
-- CREATE TABLE audit_logs (
--   ... columns ...
-- ) PARTITION BY RANGE (created_at);

-- CREATE TABLE audit_logs_2026_01 PARTITION OF audit_logs 
--   FOR VALUES FROM ('2026-01-01') TO ('2026-02-01');
-- CREATE TABLE audit_logs_2026_02 PARTITION OF audit_logs 
--   FOR VALUES FROM ('2026-02-01') TO ('2026-03-01');
-- ... continue for each month

-- Partition payments by year
-- CREATE TABLE payments (
--   ... columns ...
-- ) PARTITION BY RANGE (created_at);

-- CREATE TABLE payments_2026 PARTITION OF payments 
--   FOR VALUES FROM ('2026-01-01') TO ('2027-01-01');


-- ============================================================================
-- 7. STATISTICS & QUERY PLANNER HINTS
-- ============================================================================

-- Set higher statistics targets for frequently filtered columns
ALTER TABLE bookings ALTER COLUMN category SET STATISTICS 1000;
ALTER TABLE bookings ALTER COLUMN status SET STATISTICS 500;
ALTER TABLE bookings ALTER COLUMN payment_status SET STATISTICS 500;

ALTER TABLE tours ALTER COLUMN location SET STATISTICS 500;
ALTER TABLE tours ALTER COLUMN difficulty SET STATISTICS 200;

ALTER TABLE flights ALTER COLUMN departure_at SET STATISTICS 500;
ALTER TABLE flights ALTER COLUMN status SET STATISTICS 200;

ALTER TABLE hotels ALTER COLUMN city SET STATISTICS 500;
ALTER TABLE hotels ALTER COLUMN country SET STATISTICS 300;

ALTER TABLE reviews ALTER COLUMN category SET STATISTICS 500;
ALTER TABLE reviews ALTER COLUMN status SET STATISTICS 200;


-- ============================================================================
-- 8. AUTO-VACUUM SETTINGS FOR HIGH-WRITE TABLES
-- ============================================================================

-- Bookings: frequent inserts and updates
ALTER TABLE bookings SET (
  autovacuum_vacuum_scale_factor = 0.05,
  autovacuum_analyze_scale_factor = 0.02
);

-- Payments: frequent inserts
ALTER TABLE payments SET (
  autovacuum_vacuum_scale_factor = 0.05,
  autovacuum_analyze_scale_factor = 0.02
);

-- Audit logs: append-only, very high volume
ALTER TABLE audit_logs SET (
  autovacuum_vacuum_scale_factor = 0.02,
  autovacuum_vacuum_cost_limit = 1000
);

-- Tour schedules: frequent slot updates
ALTER TABLE tour_schedules SET (
  autovacuum_vacuum_scale_factor = 0.1
);

-- Flight fares: frequent seat availability updates
ALTER TABLE flight_fares SET (
  autovacuum_vacuum_scale_factor = 0.1
);

-- Flight seats: frequent lock/booking updates
ALTER TABLE flight_seats SET (
  autovacuum_vacuum_scale_factor = 0.1
);


-- ============================================================================
-- 9. MAINTENANCE PROCEDURES
-- ============================================================================

-- Function to rebuild all indexes (run during maintenance windows)
CREATE OR REPLACE FUNCTION rebuild_all_indexes()
RETURNS VOID AS $$
DECLARE
  idx RECORD;
BEGIN
  FOR idx IN 
    SELECT indexname, tablename 
    FROM pg_indexes 
    WHERE schemaname = 'public'
  LOOP
    EXECUTE format('REINDEX INDEX CONCURRENTLY %I', idx.indexname);
  END LOOP;
END;
$$ LANGUAGE plpgsql;

-- Function to update table statistics
CREATE OR REPLACE FUNCTION update_all_statistics()
RETURNS VOID AS $$
BEGIN
  ANALYZE bookings;
  ANALYZE payments;
  ANALYZE tours;
  ANALYZE flights;
  ANALYZE cars;
  ANALYZE hotels;
  ANALYZE rooms;
  ANALYZE reviews;
  ANALYZE audit_logs;
END;
$$ LANGUAGE plpgsql;

-- Function to archive old audit logs (move to cold storage)
CREATE OR REPLACE FUNCTION archive_old_audit_logs(months_to_keep INT DEFAULT 12)
RETURNS INT AS $$
DECLARE
  archived_count INT;
BEGIN
  -- This is a placeholder - actual implementation would move to archive table
  SELECT COUNT(*) INTO archived_count
  FROM audit_logs
  WHERE created_at < NOW() - (months_to_keep || ' months')::INTERVAL;
  
  -- Delete old logs after archiving
  -- DELETE FROM audit_logs 
  -- WHERE created_at < NOW() - (months_to_keep || ' months')::INTERVAL;
  
  RETURN archived_count;
END;
$$ LANGUAGE plpgsql;
