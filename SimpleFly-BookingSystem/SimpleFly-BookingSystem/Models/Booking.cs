using System;
using System.Collections.Generic;

namespace SimpleFly_BookingSystem.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int FlightRouteId { get; set; }
        public DateTime BookingDate { get; set; }
        public int SeatCount { get; set; }
        public string Status { get; set; } = "Confirmed"; 
        public decimal TotalFare { get; set; } 

        public virtual FlightRoute FlightRoute { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
