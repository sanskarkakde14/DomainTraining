using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.Models;

namespace SimpleFly_BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("make")]
        public ActionResult<Payment> MakePayment(int bookingId, decimal amount)
        {
            var payment = _paymentService.MakePayment(bookingId, amount);
            return Ok(payment);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("confirm")]
        public ActionResult<Payment> ConfirmPayment(int paymentId)
        {
            try
            {
                var payment = _paymentService.ConfirmPayment(paymentId);
                return Ok(payment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("refund")]
        public ActionResult<Payment> IssueRefund(int paymentId, decimal refundAmount)
        {
            try
            {
                var payment = _paymentService.IssueRefund(paymentId, refundAmount);
                return Ok(payment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

