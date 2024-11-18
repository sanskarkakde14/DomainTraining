using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SimpleFly_BookingSystem.DTOs;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;
using System.Security.Claims;

namespace SimpleFly_BookingSystem.Controllers
{   
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public IActionResult BookFlight([FromBody] BookingCreateDTO bookingDTO)
        {
            if (bookingDTO == null)
                return BadRequest("Invalid booking data.");

            try
            {
                var booking = _bookingService.BookFlight(bookingDTO);
                return Ok(booking);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{bookingId}")]
        public IActionResult GetBookingById(int bookingId)
        {
            var booking = _bookingService.GetBookingById(bookingId);

            if (booking == null)
                return NotFound("Booking not found.");

            return Ok(booking); 
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("byuser/")]
        public IActionResult GetBookingsByUser()
        {
            var userId = int.Parse(User.FindFirstValue("userId"));  // Extract userId from JWT token
            var bookings = _bookingService.GetBookingsByUser(userId);
            return Ok(bookings);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{bookingId}")]
        public IActionResult CancelBooking(int bookingId)
        {
            try
            {
                _bookingService.CancelBooking(bookingId);
                return Ok("Booking successfully canceled.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET api/bookings/history 
        [Authorize]
        [HttpGet("history")]
        public IActionResult GetBookingHistory()
        {
            var userRole = User.FindFirst("userRole")?.Value; // Extract custom role from JWT

            if (userRole == null)
            {
                return Forbid(); 
            }

            if (userRole == "Admin" || userRole == "FlightOwner")
            {
               
                var bookings = _bookingService.GetAllBookings();
                return Ok(bookings);
            }
            else if (userRole == "User")
            {
                var bookings = _bookingService.GetBookingsByUser(int.Parse(User.FindFirst("userId")?.Value));
                return Ok(bookings);
            }

            return Forbid(); // Return 403 if role is not allowed
        }
    }
}