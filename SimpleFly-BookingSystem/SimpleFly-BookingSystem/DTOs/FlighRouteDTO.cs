using System;
namespace SimpleFly_BookingSystem.DTOs
{
    public class FlightRouteDTO
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public decimal Fare { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public string FlightOwnerUsername { get; set; }
    }

}

