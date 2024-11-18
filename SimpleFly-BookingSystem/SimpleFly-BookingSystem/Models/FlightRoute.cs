using System;
using System.Collections.Generic;

namespace SimpleFly_BookingSystem.Models
{
    public partial class FlightRoute
    {
        public FlightRoute()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Fare { get; set; } 
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal CheckInWeight { get; set; }
        public decimal CabinWeight { get; set; } 

        public int? FlightOwnerId { get; set; }
        public virtual User? FlightOwner { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
