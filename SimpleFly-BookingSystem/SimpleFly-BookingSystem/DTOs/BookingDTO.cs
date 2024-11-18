using System;
namespace SimpleFly_BookingSystem.DTOs
{
    // BookingDTO.cs
    public class BookingDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserUsername { get; set; }  // Include simplified user info (username)
        public int FlightRouteId { get; set; }
        public string FlightRouteOrigin { get; set; }  // Include simplified flight route info (origin)
        public string FlightRouteDestination { get; set; }  // Include simplified flight route info (destination)
        public DateTime BookingDate { get; set; }
        public int SeatCount { get; set; }
        public decimal TotalFare { get; set; }
        public string Status { get; set; }
    }

}

