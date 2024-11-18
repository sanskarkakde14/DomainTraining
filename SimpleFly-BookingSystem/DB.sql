select * from __EFMigrationsHistory;
-- Insert Users (10 values)
INSERT INTO Users (Username, PasswordHash, Role, Email, CreatedDate, UpdatedDate)
VALUES
('sanskar', 'sanskar', 'Admin', 'sanskarkakde13@gmail.com', GETDATE(), GETDATE()),
('alice_smith', 'hashed_password_2', 'FlightOwner', 'alice.smith@example.com', GETDATE(), GETDATE()),
('bob_jones', 'hashed_password_3', 'Customer', 'bob.jones@example.com', GETDATE(), GETDATE()),
('susan_clark', 'hashed_password_4', 'Customer', 'susan.clark@example.com', GETDATE(), GETDATE()),
('charlie_brown', 'hashed_password_5', 'FlightOwner', 'charlie.brown@example.com', GETDATE(), GETDATE()),
('david_miller', 'hashed_password_6', 'Customer', 'david.miller@example.com', GETDATE(), GETDATE()),
('emma_johnson', 'hashed_password_7', 'Admin', 'emma.johnson@example.com', GETDATE(), GETDATE()),
('frank_wilson', 'hashed_password_8', 'Customer', 'frank.wilson@example.com', GETDATE(), GETDATE()),
('grace_davis', 'hashed_password_9', 'FlightOwner', 'grace.davis@example.com', GETDATE(), GETDATE()),
('hannah_white', 'hashed_password_10', 'Customer', 'hannah.white@example.com', GETDATE(), GETDATE());
select * from Users
-- Insert FlightRoutes (10 values)
INSERT INTO FlightRoutes (Origin, Destination, Date, Fare, TotalSeats, AvailableSeats, CheckInWeight, CabinWeight, FlightOwnerId)
VALUES
('New York', 'Los Angeles', '2024-12-01 10:00:00', 300.00, 100, 100, 23, 10, 2), -- FlightOwnerId = 2
('Chicago', 'Miami', '2024-12-05 14:00:00', 250.00, 150, 150, 20, 8, 3), -- FlightOwnerId = 3
('Dallas', 'San Francisco', '2024-12-10 12:00:00', 350.00, 200, 200, 25, 12, 5), -- FlightOwnerId = 5
('Atlanta', 'Orlando', '2024-12-12 08:00:00', 180.00, 120, 120, 18, 7, 4), -- FlightOwnerId = 4
('Boston', 'Seattle', '2024-12-15 16:00:00', 400.00, 180, 180, 20, 9, 6), -- FlightOwnerId = 6
('San Diego', 'Houston', '2024-12-18 18:00:00', 280.00, 130, 130, 22, 8, 2), -- FlightOwnerId = 2
('Las Vegas', 'Denver', '2024-12-20 11:00:00', 220.00, 90, 90, 21, 7, 3), -- FlightOwnerId = 3
('Phoenix', 'Salt Lake City', '2024-12-22 15:00:00', 270.00, 160, 160, 23, 10, 5), -- FlightOwnerId = 5
('Seattle', 'San Jose', '2024-12-25 09:00:00', 330.00, 110, 110, 24, 9, 4), -- FlightOwnerId = 4
('Portland', 'Minneapolis', '2024-12-28 17:00:00', 280.00, 140, 140, 22, 8, 6); -- FlightOwnerId = 6

-- Insert Bookings (10 values)
INSERT INTO Bookings (UserId, FlightRouteId, BookingDate, SeatCount, Status, TotalFare)
VALUES
(1, 1, '2024-11-10', 2, 'Confirmed', 600.00), -- John Doe, FlightRouteId = 1
(3, 2, '2024-11-11', 1, 'Confirmed', 250.00), -- Bob Jones, FlightRouteId = 2
(5, 3, '2024-11-12', 3, 'Confirmed', 1050.00), -- Charlie Brown, FlightRouteId = 3
(6, 4, '2024-11-13', 2, 'Confirmed', 360.00), -- David Miller, FlightRouteId = 4
(7, 5, '2024-11-14', 4, 'Confirmed', 1600.00), -- Emma Johnson, FlightRouteId = 5
(8, 6, '2024-11-15', 1, 'Confirmed', 280.00), -- Frank Wilson, FlightRouteId = 6
(9, 7, '2024-11-16', 2, 'Confirmed', 440.00), -- Grace Davis, FlightRouteId = 7
(10, 8, '2024-11-17', 1, 'Confirmed', 220.00), -- Hannah White, FlightRouteId = 8
(2, 9, '2024-11-18', 3, 'Confirmed', 810.00), -- Alice Smith, FlightRouteId = 9
(4, 10, '2024-11-19', 2, 'Confirmed', 560.00); -- Susan Clark, FlightRouteId = 10

-- Insert Payments (10 values)
INSERT INTO Payments (UserId, BookingId, Amount, PaymentStatus, PaymentDate, RefundAmount, RefundStatus)
VALUES
(1, 1, 600.00, 'Completed', '2024-11-10', NULL, NULL),
(3, 2, 250.00, 'Completed', '2024-11-11', NULL, NULL),
(5, 3, 1050.00, 'Completed', '2024-11-12', NULL, NULL),
(6, 4, 360.00, 'Completed', '2024-11-13', NULL, NULL),
(7, 5, 1600.00, 'Completed', '2024-11-14', NULL, NULL),
(8, 6, 280.00, 'Completed', '2024-11-15', NULL, NULL),
(9, 7, 440.00, 'Completed', '2024-11-16', NULL, NULL),
(10, 8, 220.00, 'Completed', '2024-11-17', NULL, NULL),
(2, 9, 810.00, 'Completed', '2024-11-18', NULL, NULL),
(4, 10, 560.00, 'Completed', '2024-11-19', NULL, 'Done');

select * from Users
