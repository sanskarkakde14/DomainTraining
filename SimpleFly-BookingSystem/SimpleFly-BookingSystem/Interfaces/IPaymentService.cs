using System;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Interfaces
{
	public interface IPaymentService
	{
        Payment MakePayment(int bookingId, decimal amount);
        Payment IssueRefund(int paymentId, decimal refundAmount);
        Payment ConfirmPayment(int paymentId);
    }
}

