using System;
using System.Collections.Generic;

namespace SimpleFly_BookingSystem.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            FlightRoutes = new HashSet<FlightRoute>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Customer"; 
        public string Email { get; set; } = null!; 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow; 

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<FlightRoute> FlightRoutes { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
