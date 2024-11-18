using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleFly_BookingSystem.Data;
using SimpleFly_BookingSystem.DTOs;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Services
{
    public class BookingService : IBookingService
    {
        private readonly SimplyFlyContext _context;

        public BookingService(SimplyFlyContext context)
        {
            _context = context;
        }

        public BookingDTO BookFlight(BookingCreateDTO bookingDTO)
        {
            var user = _context.Users.Find(bookingDTO.UserId);
            var flightRoute = _context.FlightRoutes.Find(bookingDTO.FlightRouteId);

            if (user == null || flightRoute == null)
                throw new ArgumentException("Invalid UserId or FlightRouteId.");

            if (bookingDTO.SeatCount <= 0 || bookingDTO.SeatCount > flightRoute.AvailableSeats)
                throw new InvalidOperationException("Not enough available seats.");

            decimal totalFare = bookingDTO.SeatCount * flightRoute.Fare;

            var booking = new Booking
            {
                UserId = bookingDTO.UserId,
                FlightRouteId = bookingDTO.FlightRouteId,
                BookingDate = DateTime.UtcNow,
                Status = "Confirmed",
                SeatCount = bookingDTO.SeatCount,
                TotalFare = totalFare
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    flightRoute.AvailableSeats -= bookingDTO.SeatCount;
                    _context.FlightRoutes.Update(flightRoute);

                    _context.Bookings.Add(booking);
                    _context.SaveChanges();

                    if (!ProcessPayment(bookingDTO.UserId, bookingDTO.PaymentAmount))
                    {
                        _context.Bookings.Remove(booking);
                        flightRoute.AvailableSeats += bookingDTO.SeatCount;
                        _context.SaveChanges();

                        transaction.Rollback();
                        throw new InvalidOperationException("Payment processing failed.");
                    }

                    transaction.Commit();

                    return new BookingDTO
                    {
                        Id = booking.Id,
                        UserId = booking.UserId,
                        UserUsername = user.Username,  
                        FlightRouteId = booking.FlightRouteId,
                        FlightRouteOrigin = flightRoute.Origin,  
                        FlightRouteDestination = flightRoute.Destination,
                        BookingDate = booking.BookingDate,
                        SeatCount = booking.SeatCount,
                        TotalFare = booking.TotalFare,
                        Status = booking.Status
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("An error occurred during the booking process.", ex);
                }
            }
        }

        public IEnumerable<BookingDTO> GetBookingsByUser(int userId)
        {
            var bookings = _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.FlightRoute)
                .ToList();

            return bookings.Select(b => new BookingDTO
            {
                Id = b.Id,
                UserId = b.UserId,
                UserUsername = b.User.Username,  
                FlightRouteId = b.FlightRouteId,
                FlightRouteOrigin = b.FlightRoute.Origin,  
                FlightRouteDestination = b.FlightRoute.Destination,
                BookingDate = b.BookingDate,
                SeatCount = b.SeatCount,
                TotalFare = b.TotalFare,
                Status = b.Status
            }).ToList();
        }
        public IEnumerable<BookingDTO> GetAllBookings()
        {
            var bookings = _context.Bookings
                .Include(b => b.User)  // Include user info to show username
                .Include(b => b.FlightRoute)  // Include flight route info
                .ToList();

            return bookings.Select(b => new BookingDTO
            {
                Id = b.Id,
                UserId = b.UserId,
                UserUsername = b.User.Username,  // Show the username for Admin/FlightOwner
                FlightRouteId = b.FlightRouteId,
                FlightRouteOrigin = b.FlightRoute.Origin,
                FlightRouteDestination = b.FlightRoute.Destination,
                BookingDate = b.BookingDate,
                SeatCount = b.SeatCount,
                TotalFare = b.TotalFare,
                Status = b.Status
            }).ToList();
        }
        public BookingDTO GetBookingById(int bookingId)
        {
            var booking = _context.Bookings
                .Where(b => b.Id == bookingId)
                .Include(b => b.FlightRoute)  
                .ThenInclude(fr => fr.FlightOwner)  
                .Include(b => b.User)  
                .FirstOrDefault();

            if (booking == null)
            {
                return null; 
            }

            return new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                UserUsername = booking.User?.Username,  
                FlightRouteId = booking.FlightRouteId,
                FlightRouteOrigin = booking.FlightRoute?.Origin,  
                FlightRouteDestination = booking.FlightRoute?.Destination,  
                BookingDate = booking.BookingDate,
                SeatCount = booking.SeatCount,
                TotalFare = booking.TotalFare,
                Status = booking.Status
            };
        }

        public bool CancelBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);

            if (booking == null || booking.Status != "Confirmed")
                throw new InvalidOperationException("Booking not found or already canceled.");

            booking.Status = "Canceled";
            _context.SaveChanges();

            RefundPayment(booking.UserId, booking.FlightRoute.Fare);

            var flightRoute = _context.FlightRoutes.Find(booking.FlightRouteId);
            if (flightRoute != null)
            {
                flightRoute.AvailableSeats += booking.SeatCount;
                _context.SaveChanges();
            }

            return true;
        }

        // Process payment (dummy implementation)
        private bool ProcessPayment(int userId, decimal amount)
        {
            return true; 
        }

        // Refund payment (dummy implementation)
        private void RefundPayment(int userId, decimal amount)
        {
            //return true;
        }
    }
}
