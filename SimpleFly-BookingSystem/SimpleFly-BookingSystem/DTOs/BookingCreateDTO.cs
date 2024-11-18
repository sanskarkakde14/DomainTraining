using System;
namespace SimpleFly_BookingSystem.DTOs
{
    public class BookingCreateDTO
    {
        public int UserId { get; set; }
        public int FlightRouteId { get; set; }
        public int SeatCount { get; set; }
        public decimal PaymentAmount { get; set; } // Amount for payment
    }

}

