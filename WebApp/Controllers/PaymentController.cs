using C.Service.Payment.CibPay.Implementations;
using E.Application.DonationService;
using E.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Views.ViewModels;

namespace WebApp.Controllers;


public class PaymentController : Controller
{
    IDonateService _donateService;
    CibPayService _cibPayService;

    public PaymentController(IDonateService donateService, CibPayService cibPayService)
    {
        _donateService = donateService;
        _cibPayService = cibPayService;
    }

    [HttpPost]
    [Route("1qwe")]
    public async Task<IActionResult> CreateAsync([FromBody] AmountDto amount)
    {
        return Ok(await _donateService.CreateOrderAsync(amount.Amount));
    }

    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> GetPaymentInfo([FromQuery] string order_id)
    {
        var paymentResult = await _cibPayService.GetOrderInfoAsync(order_id);

        if (!string.IsNullOrEmpty(paymentResult.failure_message))
        {
            var rejectModel = new RejectViewModel(paymentResult.failure_message);
            return View("Reject", rejectModel);
        }

        var result = await _donateService.CreateFinishedOrderAsync(order_id);

        if (!result)
        {
            var rejectModel = new RejectViewModel("Something went wrong");

            return View("Reject", rejectModel);
        }

        var successModel = new SuccessViewModel(paymentResult.Data.amount);

        return View("Success", successModel);
    }

}
