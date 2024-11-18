using System;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using SimpleFly_BookingSystem.Data;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly SimplyFlyContext _context;

        public PaymentService(SimplyFlyContext context)
        {
            _context = context;
        }

        public Payment MakePayment(int bookingId, decimal amount)
        {
            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = "Pending" 
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment ConfirmPayment(int paymentId)
        {
            var payment = _context.Payments
                .Include(p => p.Booking) 
                .FirstOrDefault(p => p.Id == paymentId);

            if (payment == null)
                throw new ArgumentException("Invalid Payment ID.");

            
            payment.PaymentStatus = "Completed";

           
            if (payment.Booking != null)
                payment.Booking.Status = "Confirmed";

            _context.SaveChanges();
            return payment;
        }

        public Payment IssueRefund(int paymentId, decimal refundAmount)
        {
            var payment = _context.Payments
                .Include(p => p.Booking) 
                .FirstOrDefault(p => p.Id == paymentId);

            if (payment == null)
                throw new ArgumentException("Invalid Payment ID.");

            if (payment.PaymentStatus != "Completed")
                throw new InvalidOperationException("Refunds can only be issued for completed payments.");

            payment.RefundAmount = refundAmount;
            payment.RefundStatus = "Refunded";

            _context.SaveChanges();
            return payment;
        }

        
    }
}
