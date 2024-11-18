using System;

namespace SimpleFly_BookingSystem.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = "Pending"; 
        public DateTime PaymentDate { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? RefundStatus { get; set; } = "Not Initiated"; 

        public virtual Booking Booking { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
