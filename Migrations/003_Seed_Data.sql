-- ============================================================================
-- Travel Marketplace - Seed Data
-- PostgreSQL Migration Script
-- Generated: 2026-02-05
-- ============================================================================

-- ============================================================================
-- 1. CATEGORIES (Fixed 4)
-- ============================================================================

INSERT INTO categories (id, key, title, description, image, created_at, updated_at) VALUES
(1, 'tours', 'Tours & Activities', 'Discover amazing tours and activities worldwide', 'https://cdn.example.com/categories/tours.jpg', NOW(), NOW()),
(2, 'flights', 'Flights', 'Book flights to destinations around the globe', 'https://cdn.example.com/categories/flights.jpg', NOW(), NOW()),
(3, 'cars', 'Car Rentals', 'Rent cars at great prices', 'https://cdn.example.com/categories/cars.jpg', NOW(), NOW()),
(4, 'hotels', 'Hotels & Accommodations', 'Find perfect accommodations for your trip', 'https://cdn.example.com/categories/hotels.jpg', NOW(), NOW());


-- ============================================================================
-- 2. USERS (Sample: 20 users - 2 admin, 18 regular)
-- ============================================================================

INSERT INTO users (id, first_name, last_name, email, password_hash, phone, country, city, profile_picture, is_verified, status, created_at, updated_at) VALUES
-- Admin Users
(1, 'Admin', 'Super', 'admin@travelmarket.com', '$2a$12$hash1', '+1234567890', 'USA', 'New York', NULL, true, 0, NOW(), NOW()),
(2, 'System', 'Admin', 'system@travelmarket.com', '$2a$12$hash2', '+1234567891', 'USA', 'San Francisco', NULL, true, 0, NOW(), NOW()),
-- Regular Users (Verified)
(3, 'John', 'Doe', 'john.doe@email.com', '$2a$12$hash3', '+1555123001', 'USA', 'Los Angeles', 'https://cdn.example.com/users/3.jpg', true, 0, NOW(), NOW()),
(4, 'Jane', 'Smith', 'jane.smith@email.com', '$2a$12$hash4', '+44207123001', 'UK', 'London', 'https://cdn.example.com/users/4.jpg', true, 0, NOW(), NOW()),
(5, 'Ahmed', 'Hassan', 'ahmed.h@email.com', '$2a$12$hash5', '+971501234001', 'UAE', 'Dubai', NULL, true, 0, NOW(), NOW()),
(6, 'Yuki', 'Tanaka', 'yuki.t@email.com', '$2a$12$hash6', '+81901234001', 'Japan', 'Tokyo', 'https://cdn.example.com/users/6.jpg', true, 0, NOW(), NOW()),
(7, 'Marie', 'Dubois', 'marie.d@email.com', '$2a$12$hash7', '+33612345001', 'France', 'Paris', NULL, true, 0, NOW(), NOW()),
(8, 'Hans', 'Mueller', 'hans.m@email.com', '$2a$12$hash8', '+49151234001', 'Germany', 'Berlin', 'https://cdn.example.com/users/8.jpg', true, 0, NOW(), NOW()),
(9, 'Sofia', 'Garcia', 'sofia.g@email.com', '$2a$12$hash9', '+34612345001', 'Spain', 'Madrid', NULL, true, 0, NOW(), NOW()),
(10, 'Luca', 'Rossi', 'luca.r@email.com', '$2a$12$hash10', '+39321234001', 'Italy', 'Rome', 'https://cdn.example.com/users/10.jpg', true, 0, NOW(), NOW()),
-- Regular Users (Unverified)
(11, 'Mike', 'Brown', 'mike.b@email.com', '$2a$12$hash11', '+1555123011', 'USA', 'Chicago', NULL, false, 0, NOW(), NOW()),
(12, 'Sarah', 'Wilson', 'sarah.w@email.com', '$2a$12$hash12', '+1555123012', 'Canada', 'Toronto', NULL, false, 0, NOW(), NOW()),
(13, 'Chen', 'Wei', 'chen.w@email.com', '$2a$12$hash13', '+8613812345001', 'China', 'Beijing', NULL, false, 0, NOW(), NOW()),
(14, 'Priya', 'Sharma', 'priya.s@email.com', '$2a$12$hash14', '+919876543001', 'India', 'Mumbai', NULL, false, 0, NOW(), NOW()),
(15, 'Kim', 'Park', 'kim.p@email.com', '$2a$12$hash15', '+821012345001', 'South Korea', 'Seoul', NULL, false, 0, NOW(), NOW()),
-- Social Login Users
(16, 'Alex', 'Johnson', 'alex.j@gmail.com', NULL, NULL, 'USA', 'Seattle', 'https://cdn.example.com/users/16.jpg', true, 0, NOW(), NOW()),
(17, 'Emma', 'Taylor', 'emma.t@gmail.com', NULL, NULL, 'UK', 'Manchester', 'https://cdn.example.com/users/17.jpg', true, 0, NOW(), NOW()),
(18, 'Omar', 'Khalil', 'omar.k@gmail.com', NULL, NULL, 'Egypt', 'Cairo', NULL, true, 0, NOW(), NOW()),
(19, 'Anna', 'Kowalski', 'anna.k@gmail.com', NULL, NULL, 'Poland', 'Warsaw', 'https://cdn.example.com/users/19.jpg', true, 0, NOW(), NOW()),
(20, 'Carlos', 'Martinez', 'carlos.m@gmail.com', NULL, NULL, 'Mexico', 'Mexico City', NULL, true, 0, NOW(), NOW());


-- ============================================================================
-- 3. CAR BRANDS (20 brands)
-- ============================================================================

INSERT INTO car_brands (id, name, logo_url, created_at, updated_at) VALUES
(1, 'Toyota', 'https://cdn.example.com/brands/toyota.png', NOW(), NOW()),
(2, 'BMW', 'https://cdn.example.com/brands/bmw.png', NOW(), NOW()),
(3, 'Mercedes-Benz', 'https://cdn.example.com/brands/mercedes.png', NOW(), NOW()),
(4, 'Audi', 'https://cdn.example.com/brands/audi.png', NOW(), NOW()),
(5, 'Tesla', 'https://cdn.example.com/brands/tesla.png', NOW(), NOW()),
(6, 'Ford', 'https://cdn.example.com/brands/ford.png', NOW(), NOW()),
(7, 'Honda', 'https://cdn.example.com/brands/honda.png', NOW(), NOW()),
(8, 'Nissan', 'https://cdn.example.com/brands/nissan.png', NOW(), NOW()),
(9, 'Chevrolet', 'https://cdn.example.com/brands/chevrolet.png', NOW(), NOW()),
(10, 'Hyundai', 'https://cdn.example.com/brands/hyundai.png', NOW(), NOW()),
(11, 'Kia', 'https://cdn.example.com/brands/kia.png', NOW(), NOW()),
(12, 'Volkswagen', 'https://cdn.example.com/brands/vw.png', NOW(), NOW()),
(13, 'Porsche', 'https://cdn.example.com/brands/porsche.png', NOW(), NOW()),
(14, 'Jaguar', 'https://cdn.example.com/brands/jaguar.png', NOW(), NOW()),
(15, 'Land Rover', 'https://cdn.example.com/brands/landrover.png', NOW(), NOW()),
(16, 'Lexus', 'https://cdn.example.com/brands/lexus.png', NOW(), NOW()),
(17, 'Mazda', 'https://cdn.example.com/brands/mazda.png', NOW(), NOW()),
(18, 'Subaru', 'https://cdn.example.com/brands/subaru.png', NOW(), NOW()),
(19, 'Volvo', 'https://cdn.example.com/brands/volvo.png', NOW(), NOW()),
(20, 'Jeep', 'https://cdn.example.com/brands/jeep.png', NOW(), NOW());


-- ============================================================================
-- 4. CARRIERS (Airlines - 20 carriers)
-- ============================================================================

INSERT INTO carriers (id, name, iata_code, logo_url, phone, email, website, created_at, updated_at) VALUES
(1, 'Emirates', 'EK', 'https://cdn.example.com/airlines/emirates.png', '+971600555555', 'info@emirates.com', 'https://emirates.com', NOW(), NOW()),
(2, 'Qatar Airways', 'QR', 'https://cdn.example.com/airlines/qatar.png', '+97440230000', 'info@qatarairways.com', 'https://qatarairways.com', NOW(), NOW()),
(3, 'Singapore Airlines', 'SQ', 'https://cdn.example.com/airlines/singapore.png', '+6562238888', 'info@singaporeair.com', 'https://singaporeair.com', NOW(), NOW()),
(4, 'Lufthansa', 'LH', 'https://cdn.example.com/airlines/lufthansa.png', '+496986799799', 'info@lufthansa.com', 'https://lufthansa.com', NOW(), NOW()),
(5, 'British Airways', 'BA', 'https://cdn.example.com/airlines/british.png', '+442084342222', 'info@britishairways.com', 'https://ba.com', NOW(), NOW()),
(6, 'Air France', 'AF', 'https://cdn.example.com/airlines/airfrance.png', '+33892702654', 'info@airfrance.com', 'https://airfrance.com', NOW(), NOW()),
(7, 'KLM', 'KL', 'https://cdn.example.com/airlines/klm.png', '+31206490787', 'info@klm.com', 'https://klm.com', NOW(), NOW()),
(8, 'Turkish Airlines', 'TK', 'https://cdn.example.com/airlines/turkish.png', '+902124446328', 'info@thy.com', 'https://turkishairlines.com', NOW(), NOW()),
(9, 'United Airlines', 'UA', 'https://cdn.example.com/airlines/united.png', '+18008648331', 'info@united.com', 'https://united.com', NOW(), NOW()),
(10, 'Delta Air Lines', 'DL', 'https://cdn.example.com/airlines/delta.png', '+18002211212', 'info@delta.com', 'https://delta.com', NOW(), NOW()),
(11, 'American Airlines', 'AA', 'https://cdn.example.com/airlines/american.png', '+18004337300', 'info@aa.com', 'https://aa.com', NOW(), NOW()),
(12, 'Etihad Airways', 'EY', 'https://cdn.example.com/airlines/etihad.png', '+97125990000', 'info@etihad.com', 'https://etihad.com', NOW(), NOW()),
(13, 'Japan Airlines', 'JL', 'https://cdn.example.com/airlines/jal.png', '+81332115111', 'info@jal.com', 'https://jal.com', NOW(), NOW()),
(14, 'ANA', 'NH', 'https://cdn.example.com/airlines/ana.png', '+81357898577', 'info@ana.com', 'https://ana.co.jp', NOW(), NOW()),
(15, 'Cathay Pacific', 'CX', 'https://cdn.example.com/airlines/cathay.png', '+85227478888', 'info@cathaypacific.com', 'https://cathaypacific.com', NOW(), NOW()),
(16, 'Swiss', 'LX', 'https://cdn.example.com/airlines/swiss.png', '+41848700700', 'info@swiss.com', 'https://swiss.com', NOW(), NOW()),
(17, 'Air Canada', 'AC', 'https://cdn.example.com/airlines/aircanada.png', '+18882472262', 'info@aircanada.com', 'https://aircanada.com', NOW(), NOW()),
(18, 'Qantas', 'QF', 'https://cdn.example.com/airlines/qantas.png', '+61293411311', 'info@qantas.com', 'https://qantas.com', NOW(), NOW()),
(19, 'Korean Air', 'KE', 'https://cdn.example.com/airlines/korean.png', '+8215882001', 'info@koreanair.com', 'https://koreanair.com', NOW(), NOW()),
(20, 'Thai Airways', 'TG', 'https://cdn.example.com/airlines/thai.png', '+6623560111', 'info@thaiairways.com', 'https://thaiairways.com', NOW(), NOW());


-- ============================================================================
-- 5. AIRPORTS (30 major airports)
-- ============================================================================

INSERT INTO airports (id, iata_code, name, city, country, latitude, longitude, timezone, created_at, updated_at) VALUES
(1, 'JFK', 'John F. Kennedy International', 'New York', 'USA', 40.6413, -73.7781, 'America/New_York', NOW(), NOW()),
(2, 'LAX', 'Los Angeles International', 'Los Angeles', 'USA', 33.9416, -118.4085, 'America/Los_Angeles', NOW(), NOW()),
(3, 'ORD', 'O''Hare International', 'Chicago', 'USA', 41.9742, -87.9073, 'America/Chicago', NOW(), NOW()),
(4, 'LHR', 'Heathrow', 'London', 'UK', 51.4700, -0.4543, 'Europe/London', NOW(), NOW()),
(5, 'CDG', 'Charles de Gaulle', 'Paris', 'France', 49.0097, 2.5479, 'Europe/Paris', NOW(), NOW()),
(6, 'FRA', 'Frankfurt', 'Frankfurt', 'Germany', 50.0379, 8.5622, 'Europe/Berlin', NOW(), NOW()),
(7, 'DXB', 'Dubai International', 'Dubai', 'UAE', 25.2532, 55.3657, 'Asia/Dubai', NOW(), NOW()),
(8, 'SIN', 'Changi', 'Singapore', 'Singapore', 1.3644, 103.9915, 'Asia/Singapore', NOW(), NOW()),
(9, 'HND', 'Haneda', 'Tokyo', 'Japan', 35.5494, 139.7798, 'Asia/Tokyo', NOW(), NOW()),
(10, 'NRT', 'Narita', 'Tokyo', 'Japan', 35.7720, 140.3929, 'Asia/Tokyo', NOW(), NOW()),
(11, 'HKG', 'Hong Kong International', 'Hong Kong', 'Hong Kong', 22.3080, 113.9185, 'Asia/Hong_Kong', NOW(), NOW()),
(12, 'AMS', 'Schiphol', 'Amsterdam', 'Netherlands', 52.3086, 4.7639, 'Europe/Amsterdam', NOW(), NOW()),
(13, 'IST', 'Istanbul', 'Istanbul', 'Turkey', 41.2753, 28.7519, 'Europe/Istanbul', NOW(), NOW()),
(14, 'DOH', 'Hamad International', 'Doha', 'Qatar', 25.2731, 51.6081, 'Asia/Qatar', NOW(), NOW()),
(15, 'SYD', 'Sydney Kingsford Smith', 'Sydney', 'Australia', -33.9399, 151.1753, 'Australia/Sydney', NOW(), NOW()),
(16, 'ICN', 'Incheon International', 'Seoul', 'South Korea', 37.4602, 126.4407, 'Asia/Seoul', NOW(), NOW()),
(17, 'PEK', 'Beijing Capital', 'Beijing', 'China', 40.0799, 116.6031, 'Asia/Shanghai', NOW(), NOW()),
(18, 'BKK', 'Suvarnabhumi', 'Bangkok', 'Thailand', 13.6900, 100.7501, 'Asia/Bangkok', NOW(), NOW()),
(19, 'FCO', 'Fiumicino', 'Rome', 'Italy', 41.8003, 12.2389, 'Europe/Rome', NOW(), NOW()),
(20, 'MAD', 'Adolfo Suárez Madrid-Barajas', 'Madrid', 'Spain', 40.4983, -3.5676, 'Europe/Madrid', NOW(), NOW()),
(21, 'MIA', 'Miami International', 'Miami', 'USA', 25.7959, -80.2870, 'America/New_York', NOW(), NOW()),
(22, 'SFO', 'San Francisco International', 'San Francisco', 'USA', 37.6213, -122.3790, 'America/Los_Angeles', NOW(), NOW()),
(23, 'DEN', 'Denver International', 'Denver', 'USA', 39.8561, -104.6737, 'America/Denver', NOW(), NOW()),
(24, 'YYZ', 'Toronto Pearson', 'Toronto', 'Canada', 43.6777, -79.6248, 'America/Toronto', NOW(), NOW()),
(25, 'MUC', 'Munich', 'Munich', 'Germany', 48.3537, 11.7750, 'Europe/Berlin', NOW(), NOW()),
(26, 'ZRH', 'Zurich', 'Zurich', 'Switzerland', 47.4582, 8.5555, 'Europe/Zurich', NOW(), NOW()),
(27, 'BCN', 'Barcelona El Prat', 'Barcelona', 'Spain', 41.2974, 2.0833, 'Europe/Madrid', NOW(), NOW()),
(28, 'CAI', 'Cairo International', 'Cairo', 'Egypt', 30.1219, 31.4056, 'Africa/Cairo', NOW(), NOW()),
(29, 'DEL', 'Indira Gandhi International', 'New Delhi', 'India', 28.5562, 77.1000, 'Asia/Kolkata', NOW(), NOW()),
(30, 'KUL', 'Kuala Lumpur International', 'Kuala Lumpur', 'Malaysia', 2.7456, 101.7099, 'Asia/Kuala_Lumpur', NOW(), NOW());


-- ============================================================================
-- 6. SAMPLE TOURS (10 tours)
-- ============================================================================

INSERT INTO tours (id, title, slug, summary, full_description, location, duration_days, duration_hours, highlights_json, activities_json, inclusions_json, exclusions_json, cancellation_policy, languages_json, difficulty, provider_name, provider_contact_json, tags_json, recommended, avg_rating, review_count, is_active, created_at, updated_at) VALUES
(1, 'Paris City Highlights', 'paris-city-highlights', 'Discover the magic of Paris with this full-day tour', 'Experience the best of Paris including the Eiffel Tower, Louvre Museum, and Notre-Dame Cathedral.', 'Paris, France', 1, 8, '["Eiffel Tower", "Louvre Museum", "Notre-Dame", "Champs-Élysées"]', '["Sightseeing", "Photography", "Museums"]', '["Professional guide", "Transportation", "Skip-the-line tickets"]', '["Meals", "Personal expenses"]', 'Free cancellation up to 24 hours before', '["English", "French", "Spanish"]', 0, 'Paris Tours LLC', '{"phone": "+33612345678", "email": "info@paristours.com"}', '["city-tour", "culture", "history"]', true, 4.75, 342, true, NOW(), NOW()),
(2, 'Dubai Desert Safari', 'dubai-desert-safari', 'Experience the Arabian desert adventure', 'Unforgettable desert experience with dune bashing, camel rides, and BBQ dinner under the stars.', 'Dubai, UAE', 1, 6, '["Dune Bashing", "Camel Ride", "BBQ Dinner", "Belly Dance Show"]', '["Adventure", "Cultural Experience", "Photography"]', '["Hotel pickup", "4x4 safari", "BBQ dinner", "Entertainment"]', '["Alcoholic beverages", "Tips"]', 'Free cancellation up to 48 hours before', '["English", "Arabic"]', 1, 'Desert Adventures', '{"phone": "+971501234567", "email": "info@desertadv.ae"}', '["desert", "adventure", "culture"]', true, 4.85, 567, true, NOW(), NOW()),
(3, 'Tokyo Food Tour', 'tokyo-food-tour', 'Taste authentic Japanese cuisine', 'Explore hidden food gems in Tokyo with local experts. Sample sushi, ramen, tempura and more.', 'Tokyo, Japan', 1, 4, '["Tsukiji Market", "Ramen tasting", "Street food", "Sake bar"]', '["Food tasting", "Walking tour", "Cultural Experience"]', '["Professional food guide", "10+ food tastings", "Drinks"]', '["Transportation to meeting point"]', 'Free cancellation up to 24 hours before', '["English", "Japanese"]', 0, 'Tokyo Foodies', '{"phone": "+81901234567", "email": "info@tokyofoodies.jp"}', '["food", "culture", "walking"]', true, 4.92, 234, true, NOW(), NOW()),
(4, 'Rome Colosseum Tour', 'rome-colosseum-tour', 'Skip the line at the iconic Colosseum', 'Explore the ancient Colosseum, Roman Forum, and Palatine Hill with expert historian guides.', 'Rome, Italy', 1, 3, '["Colosseum Arena Floor", "Roman Forum", "Palatine Hill"]', '["History", "Archaeology", "Photography"]', '["Expert guide", "Skip-the-line access", "Audio guide"]', '["Food and drinks", "Tips"]', 'Free cancellation up to 24 hours before', '["English", "Italian", "German"]', 0, 'Roma Tours', '{"phone": "+393201234567", "email": "info@romatours.it"}', '["history", "ancient", "culture"]', true, 4.68, 456, true, NOW(), NOW()),
(5, 'New York City Helicopter Tour', 'nyc-helicopter-tour', 'See NYC from above', 'Breathtaking helicopter tour over Manhattan, Statue of Liberty, and Central Park.', 'New York, USA', 1, 1, '["Statue of Liberty", "Manhattan Skyline", "Central Park", "Empire State"]', '["Scenic Flight", "Photography"]', '["15-minute flight", "Safety briefing", "Souvenir photo"]', '["Hotel transfers", "Gratuities"]', 'Free cancellation up to 72 hours before', '["English"]', 0, 'NYC Helicopters', '{"phone": "+12125551234", "email": "info@nychelitours.com"}', '["aerial", "scenic", "luxury"]', true, 4.95, 189, true, NOW(), NOW()),
(6, 'Bali Rice Terraces Trek', 'bali-rice-terraces-trek', 'Trek through stunning rice paddies', 'Full-day trek through Tegallalang Rice Terraces with traditional lunch and coffee plantation visit.', 'Bali, Indonesia', 1, 8, '["Tegallalang Terraces", "Coffee Plantation", "Traditional Lunch"]', '["Trekking", "Nature", "Cultural Experience"]', '["Hotel pickup", "Guide", "Lunch", "Entrance fees"]', '["Personal expenses"]', 'Free cancellation up to 24 hours before', '["English", "Indonesian"]', 1, 'Bali Trekkers', '{"phone": "+62811234567", "email": "info@balitrekkers.com"}', '["nature", "trekking", "culture"]', false, 4.72, 298, true, NOW(), NOW()),
(7, 'London Harry Potter Tour', 'london-harry-potter-tour', 'Magical Harry Potter film locations', 'Visit iconic Harry Potter filming locations including Platform 9¾ and Leadenhall Market.', 'London, UK', 1, 3, '["Platform 9¾", "Leadenhall Market", "Millennium Bridge", "Borough Market"]', '["Walking Tour", "Film Locations", "Photography"]', '["Expert Harry Potter guide", "Photo opportunities"]', '["Transportation", "Food"]', 'Free cancellation up to 24 hours before', '["English"]', 0, 'Wizard Tours London', '{"phone": "+442071234567", "email": "info@wizardtours.co.uk"}', '["film", "harry-potter", "walking"]', true, 4.88, 412, true, NOW(), NOW()),
(8, 'Grand Canyon Day Trip', 'grand-canyon-day-trip', 'Marvel at the Grand Canyon', 'Full-day trip from Las Vegas to the Grand Canyon South Rim with stops at Hoover Dam.', 'Las Vegas, USA', 1, 14, '["Grand Canyon South Rim", "Hoover Dam", "Route 66"]', '["Sightseeing", "Photography", "Nature"]', '["Luxury bus transport", "Lunch", "National Park fee"]', '["Helicopter upgrade", "Tips"]', 'Free cancellation up to 48 hours before', '["English", "Spanish"]', 0, 'Vegas Tours', '{"phone": "+17025551234", "email": "info@vegastours.com"}', '["nature", "scenic", "day-trip"]', true, 4.65, 523, true, NOW(), NOW()),
(9, 'Santorini Sunset Cruise', 'santorini-sunset-cruise', 'Sail the caldera at sunset', 'Catamaran cruise around Santorini caldera with swimming stops, BBQ dinner, and stunning sunset views.', 'Santorini, Greece', 1, 5, '["Caldera Views", "Hot Springs", "Red Beach", "Sunset"]', '["Sailing", "Swimming", "Snorkeling"]', '["Catamaran cruise", "BBQ dinner", "Drinks", "Snorkeling gear"]', '["Hotel transfers"]', 'Free cancellation up to 24 hours before', '["English", "Greek"]', 0, 'Santorini Sailing', '{"phone": "+302286012345", "email": "info@santorinisail.gr"}', '["sailing", "sunset", "romantic"]', true, 4.90, 367, true, NOW(), NOW()),
(10, 'Machu Picchu Full Day', 'machu-picchu-full-day', 'Explore the lost city of the Incas', 'Train journey to Machu Picchu with guided tour of the ancient citadel.', 'Cusco, Peru', 1, 16, '["Machu Picchu", "Sacred Valley", "Train Journey"]', '["History", "Archaeology", "Photography", "Trekking"]', '["Train tickets", "Expert guide", "Entrance fee", "Lunch"]', '["Tips", "Personal expenses"]', 'Free cancellation up to 72 hours before', '["English", "Spanish"]', 2, 'Peru Explorer', '{"phone": "+5184251234", "email": "info@peruexplorer.pe"}', '["history", "unesco", "adventure"]', true, 4.88, 445, true, NOW(), NOW());


-- ============================================================================
-- 7. SAMPLE HOTELS (10 hotels)
-- ============================================================================

INSERT INTO hotels (id, name, slug, description, address, city, country, latitude, longitude, main_image, amenities_json, star_rating, policies_json, contact_info_json, avg_rating, review_count, is_active, created_at, updated_at) VALUES
(1, 'The Ritz Paris', 'the-ritz-paris', 'Legendary palace hotel on Place Vendôme', '15 Place Vendôme', 'Paris', 'France', 48.8682, 2.3281, 'https://cdn.example.com/hotels/ritz-paris.jpg', '["Pool", "Spa", "Restaurant", "Bar", "WiFi", "Room Service", "Concierge"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 48 hours"}', '{"phone": "+33143163030", "email": "resa@ritzparis.com"}', 4.92, 234, true, NOW(), NOW()),
(2, 'Burj Al Arab', 'burj-al-arab', 'Iconic sail-shaped luxury hotel', 'Jumeirah Beach Road', 'Dubai', 'UAE', 25.1412, 55.1855, 'https://cdn.example.com/hotels/burj.jpg', '["Pool", "Spa", "Helipad", "Butler Service", "Private Beach", "Multiple Restaurants"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 72 hours"}', '{"phone": "+97143017777", "email": "reservations@burj.com"}', 4.95, 567, true, NOW(), NOW()),
(3, 'Park Hyatt Tokyo', 'park-hyatt-tokyo', 'Contemporary luxury in Shinjuku', '3-7-1-2 Nishi Shinjuku', 'Tokyo', 'Japan', 35.6869, 139.6908, 'https://cdn.example.com/hotels/park-hyatt-tokyo.jpg', '["Pool", "Spa", "Gym", "Restaurant", "Bar", "WiFi", "Room Service"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 24 hours"}', '{"phone": "+81353221234", "email": "tokyo.park@hyatt.com"}', 4.88, 189, true, NOW(), NOW()),
(4, 'The Savoy', 'the-savoy-london', 'Historic luxury on the Thames', 'Strand', 'London', 'UK', 51.5104, -0.1206, 'https://cdn.example.com/hotels/savoy.jpg', '["Pool", "Spa", "Restaurant", "Bar", "Theatre", "WiFi", "Butler Service"]', 5.0, '{"check_in": "15:00", "check_out": "11:00", "cancellation": "Free cancellation up to 48 hours"}', '{"phone": "+442078364343", "email": "savoy@fairmont.com"}', 4.85, 345, true, NOW(), NOW()),
(5, 'Marina Bay Sands', 'marina-bay-sands', 'Iconic Singapore landmark', '10 Bayfront Avenue', 'Singapore', 'Singapore', 1.2834, 103.8607, 'https://cdn.example.com/hotels/mbs.jpg', '["Infinity Pool", "Casino", "Shopping Mall", "Multiple Restaurants", "Spa", "Skypark"]', 5.0, '{"check_in": "15:00", "check_out": "11:00", "cancellation": "Free cancellation up to 48 hours"}', '{"phone": "+6566888868", "email": "reservations@mbs.com.sg"}', 4.78, 678, true, NOW(), NOW()),
(6, 'Hotel & Esme', 'hotel-esme-miami', 'Boutique luxury in South Beach', 'Ocean Drive', 'Miami', 'USA', 25.7899, -80.1302, 'https://cdn.example.com/hotels/esme.jpg', '["Pool", "Restaurant", "Bar", "WiFi", "Beach Access", "Rooftop"]', 4.0, '{"check_in": "16:00", "check_out": "11:00", "cancellation": "Free cancellation up to 24 hours"}', '{"phone": "+13055551234", "email": "info@hotelesme.com"}', 4.45, 234, true, NOW(), NOW()),
(7, 'Raffles Singapore', 'raffles-singapore', 'Colonial grandeur since 1887', '1 Beach Road', 'Singapore', 'Singapore', 1.2946, 103.8541, 'https://cdn.example.com/hotels/raffles.jpg', '["Pool", "Spa", "Multiple Restaurants", "Bar", "Butler Service", "WiFi"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 72 hours"}', '{"phone": "+6563371886", "email": "singapore@raffles.com"}', 4.91, 412, true, NOW(), NOW()),
(8, 'Aman Tokyo', 'aman-tokyo', 'Urban sanctuary in Otemachi', '1-5-6 Otemachi', 'Tokyo', 'Japan', 35.6892, 139.7632, 'https://cdn.example.com/hotels/aman-tokyo.jpg', '["Spa", "Restaurant", "Bar", "Gym", "WiFi", "Concierge"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 48 hours"}', '{"phone": "+81352242333", "email": "amantokyo@aman.com"}', 4.96, 156, true, NOW(), NOW()),
(9, 'Four Seasons Resort Bali', 'four-seasons-bali', 'Luxury villas in the jungle', 'Sayan, Ubud', 'Bali', 'Indonesia', -8.5025, 115.2533, 'https://cdn.example.com/hotels/fs-bali.jpg', '["Private Pool", "Spa", "Restaurant", "Bar", "Yoga", "Rice Terrace Views"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 72 hours"}', '{"phone": "+62361977577", "email": "res.bali@fourseasons.com"}', 4.93, 289, true, NOW(), NOW()),
(10, 'The Plaza', 'the-plaza-nyc', 'NYC landmark since 1907', '768 Fifth Avenue', 'New York', 'USA', 40.7645, -73.9742, 'https://cdn.example.com/hotels/plaza.jpg', '["Spa", "Restaurant", "Bar", "Butler Service", "WiFi", "Fitness Center"]', 5.0, '{"check_in": "15:00", "check_out": "12:00", "cancellation": "Free cancellation up to 48 hours"}', '{"phone": "+12127591900", "email": "reservations@theplaza.com"}', 4.82, 534, true, NOW(), NOW());


-- ============================================================================
-- 8. COUPONS (10 coupons)
-- ============================================================================

INSERT INTO coupons (id, code, description, discount_type, discount_value, max_discount, min_purchase, valid_from, valid_until, usage_limit, used_count, applicable_categories_json, active, created_at, updated_at) VALUES
(1, 'WELCOME10', 'Welcome discount for new users', 0, 10.00, 50.00, 100.00, '2026-01-01', '2026-12-31', NULL, 234, NULL, true, NOW(), NOW()),
(2, 'SUMMER2026', 'Summer promotion 20% off', 0, 20.00, 100.00, 200.00, '2026-06-01', '2026-08-31', 1000, 0, NULL, true, NOW(), NOW()),
(3, 'FLIGHT50', '$50 off any flight', 1, 50.00, NULL, 300.00, '2026-01-01', '2026-06-30', 500, 123, '["flights"]', true, NOW(), NOW()),
(4, 'HOTELWEEK', '15% off hotels', 0, 15.00, 75.00, 150.00, '2026-02-01', '2026-02-28', NULL, 45, '["hotels"]', true, NOW(), NOW()),
(5, 'TOUR25', '25% off tours and activities', 0, 25.00, 100.00, 100.00, '2026-01-01', '2026-03-31', 200, 67, '["tours"]', true, NOW(), NOW()),
(6, 'CARFREE', 'Free day car rental', 1, 75.00, NULL, 225.00, '2026-01-01', '2026-12-31', 100, 12, '["cars"]', true, NOW(), NOW()),
(7, 'VIP30', 'VIP members 30% discount', 0, 30.00, 200.00, 500.00, '2026-01-01', '2026-12-31', NULL, 89, NULL, true, NOW(), NOW()),
(8, 'EXPIRED23', 'Old promo (expired)', 0, 15.00, 50.00, 100.00, '2023-01-01', '2023-12-31', NULL, 567, NULL, false, NOW(), NOW()),
(9, 'FLASH100', 'Flash sale $100 off', 1, 100.00, NULL, 500.00, '2026-02-01', '2026-02-07', 50, 50, NULL, false, NOW(), NOW()),
(10, 'LOYALTY5', 'Loyalty program 5% off', 0, 5.00, 25.00, 50.00, '2026-01-01', '2026-12-31', NULL, 1234, NULL, true, NOW(), NOW());


-- ============================================================================
-- 9. TOUR SCHEDULES (Sample for first 3 tours)
-- ============================================================================

INSERT INTO tour_schedules (id, tour_id, start_date, end_date, total_capacity, available_slots, status, created_at, updated_at) VALUES
-- Paris City Highlights schedules
(1, 1, '2026-02-15', '2026-02-15', 20, 15, 0, NOW(), NOW()),
(2, 1, '2026-02-16', '2026-02-16', 20, 20, 0, NOW(), NOW()),
(3, 1, '2026-02-17', '2026-02-17', 20, 8, 0, NOW(), NOW()),
(4, 1, '2026-02-20', '2026-02-20', 20, 0, 1, NOW(), NOW()),
-- Dubai Desert Safari schedules
(5, 2, '2026-02-15', '2026-02-15', 30, 25, 0, NOW(), NOW()),
(6, 2, '2026-02-16', '2026-02-16', 30, 30, 0, NOW(), NOW()),
(7, 2, '2026-02-17', '2026-02-17', 30, 12, 0, NOW(), NOW()),
-- Tokyo Food Tour schedules
(8, 3, '2026-02-15', '2026-02-15', 12, 8, 0, NOW(), NOW()),
(9, 3, '2026-02-16', '2026-02-16', 12, 12, 0, NOW(), NOW()),
(10, 3, '2026-02-17', '2026-02-17', 12, 0, 1, NOW(), NOW());


-- ============================================================================
-- 10. TOUR PRICE TIERS (Sample for first 3 tours)
-- ============================================================================

INSERT INTO tour_price_tiers (id, tour_id, tier_name, adult_price, child_price, infant_price, currency, created_at, updated_at) VALUES
-- Paris City Highlights
(1, 1, 'Standard', 89.00, 49.00, 0.00, 'EUR', NOW(), NOW()),
(2, 1, 'Premium', 149.00, 89.00, 0.00, 'EUR', NOW(), NOW()),
-- Dubai Desert Safari
(3, 2, 'Standard', 199.00, 149.00, 0.00, 'AED', NOW(), NOW()),
(4, 2, 'Premium', 349.00, 249.00, 0.00, 'AED', NOW(), NOW()),
(5, 2, 'VIP', 599.00, 449.00, 0.00, 'AED', NOW(), NOW()),
-- Tokyo Food Tour
(6, 3, 'Regular', 12000.00, 8000.00, 0.00, 'JPY', NOW(), NOW()),
(7, 3, 'Deluxe', 18000.00, 12000.00, 0.00, 'JPY', NOW(), NOW());


-- ============================================================================
-- 11. ROOMS (Sample for first 3 hotels)
-- ============================================================================

INSERT INTO rooms (id, hotel_id, name, description, max_adults, max_children, max_infants, bed_type, room_area_sqm, price_per_night, currency, refundable, extras_json, avg_rating, review_count, total_inventory, is_active, created_at, updated_at) VALUES
-- The Ritz Paris rooms
(1, 1, 'Superior Room', 'Elegant room with garden view', 2, 1, 1, '1 King', 45, 1200.00, 'EUR', true, '["WiFi", "Minibar", "Room Service"]', 4.88, 45, 30, true, NOW(), NOW()),
(2, 1, 'Deluxe Suite', 'Luxurious suite with sitting area', 2, 2, 1, '1 King', 75, 2500.00, 'EUR', true, '["WiFi", "Minibar", "Butler Service", "Balcony"]', 4.95, 23, 15, true, NOW(), NOW()),
(3, 1, 'Prestige Suite', 'Ultimate luxury with Place Vendôme view', 3, 2, 1, '1 King + Sofa', 120, 5000.00, 'EUR', true, '["WiFi", "Minibar", "Butler Service", "Terrace", "Jacuzzi"]', 4.98, 12, 5, true, NOW(), NOW()),
-- Burj Al Arab rooms
(4, 2, 'Deluxe Suite', 'Two-story suite with ocean views', 2, 2, 1, '1 King', 170, 3500.00, 'USD', true, '["Butler Service", "Private Bar", "Ocean View"]', 4.92, 78, 28, true, NOW(), NOW()),
(5, 2, 'Panoramic Suite', 'Full floor with 360 views', 4, 3, 2, '2 Kings', 390, 8000.00, 'USD', true, '["Butler Service", "Private Cinema", "Jacuzzi"]', 4.97, 34, 8, true, NOW(), NOW()),
(6, 2, 'Royal Suite', 'The ultimate in luxury', 6, 4, 2, '3 Kings', 780, 28000.00, 'USD', false, '["Private Elevator", "Rotating Bed", "Cinema", "Helipad Access"]', 5.00, 5, 2, true, NOW(), NOW()),
-- Park Hyatt Tokyo rooms
(7, 3, 'Park Room', 'Contemporary room with city view', 2, 1, 1, '1 King', 50, 650.00, 'USD', true, '["WiFi", "Minibar", "City View"]', 4.82, 67, 40, true, NOW(), NOW()),
(8, 3, 'Park Suite', 'Spacious suite with living area', 2, 2, 1, '1 King', 80, 1200.00, 'USD', true, '["WiFi", "Minibar", "Living Room", "Mt. Fuji View"]', 4.90, 34, 20, true, NOW(), NOW()),
(9, 3, 'Presidential Suite', 'Two-bedroom ultimate luxury', 4, 2, 2, '2 Kings', 200, 5500.00, 'USD', false, '["Private Gym", "Dining Room", "360 Views"]', 4.97, 8, 2, true, NOW(), NOW());


-- ============================================================================
-- 12. LOCATIONS (Car rental locations)
-- ============================================================================

INSERT INTO locations (id, name, address, city, country, latitude, longitude, operating_hours_json, phone, email, is_active, created_at, updated_at) VALUES
(1, 'JFK Airport', 'JFK Airport, Building 269', 'New York', 'USA', 40.6413, -73.7781, '{"mon": "00:00-24:00", "tue": "00:00-24:00", "wed": "00:00-24:00", "thu": "00:00-24:00", "fri": "00:00-24:00", "sat": "00:00-24:00", "sun": "00:00-24:00"}', '+12125551001', 'jfk@carrental.com', true, NOW(), NOW()),
(2, 'LAX Airport', 'LAX Airport Car Rental Center', 'Los Angeles', 'USA', 33.9416, -118.4085, '{"mon": "00:00-24:00", "tue": "00:00-24:00", "wed": "00:00-24:00", "thu": "00:00-24:00", "fri": "00:00-24:00", "sat": "00:00-24:00", "sun": "00:00-24:00"}', '+13105551002', 'lax@carrental.com', true, NOW(), NOW()),
(3, 'Dubai Airport', 'DXB Terminal 3', 'Dubai', 'UAE', 25.2532, 55.3657, '{"mon": "00:00-24:00", "tue": "00:00-24:00", "wed": "00:00-24:00", "thu": "00:00-24:00", "fri": "00:00-24:00", "sat": "00:00-24:00", "sun": "00:00-24:00"}', '+97142221003', 'dxb@carrental.com', true, NOW(), NOW()),
(4, 'London Heathrow', 'LHR Terminal 5', 'London', 'UK', 51.4700, -0.4543, '{"mon": "06:00-23:00", "tue": "06:00-23:00", "wed": "06:00-23:00", "thu": "06:00-23:00", "fri": "06:00-23:00", "sat": "07:00-22:00", "sun": "07:00-22:00"}', '+442081231004', 'lhr@carrental.com', true, NOW(), NOW()),
(5, 'Paris CDG', 'CDG Terminal 2F', 'Paris', 'France', 49.0097, 2.5479, '{"mon": "07:00-22:00", "tue": "07:00-22:00", "wed": "07:00-22:00", "thu": "07:00-22:00", "fri": "07:00-22:00", "sat": "08:00-20:00", "sun": "08:00-20:00"}', '+33148621005', 'cdg@carrental.fr', true, NOW(), NOW());


-- ============================================================================
-- 13. Update sequence values
-- ============================================================================

SELECT setval('users_id_seq', (SELECT MAX(id) FROM users));
SELECT setval('categories_id_seq', (SELECT MAX(id) FROM categories));
SELECT setval('car_brands_id_seq', (SELECT MAX(id) FROM car_brands));
SELECT setval('carriers_id_seq', (SELECT MAX(id) FROM carriers));
SELECT setval('airports_id_seq', (SELECT MAX(id) FROM airports));
SELECT setval('tours_id_seq', (SELECT MAX(id) FROM tours));
SELECT setval('hotels_id_seq', (SELECT MAX(id) FROM hotels));
SELECT setval('coupons_id_seq', (SELECT MAX(id) FROM coupons));
SELECT setval('tour_schedules_id_seq', (SELECT MAX(id) FROM tour_schedules));
SELECT setval('tour_price_tiers_id_seq', (SELECT MAX(id) FROM tour_price_tiers));
SELECT setval('rooms_id_seq', (SELECT MAX(id) FROM rooms));
SELECT setval('locations_id_seq', (SELECT MAX(id) FROM locations));
