using System;
using SimpleFly_BookingSystem.DTOs;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Interfaces
{
	public interface IBookingService
	{
        BookingDTO BookFlight(BookingCreateDTO bookingDTO);
        IEnumerable<BookingDTO> GetBookingsByUser(int userId);
        bool CancelBooking(int bookingId);
        BookingDTO GetBookingById(int bookingId);
        IEnumerable<BookingDTO> GetAllBookings();
    }
}

