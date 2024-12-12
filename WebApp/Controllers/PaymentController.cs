using E.Application.PaymentProccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.CibPayIntegration.Implementations;
using WebApp.View.ViewModels;

namespace WebApp.Controllers;


[Route("api/[controller]")]
public class PaymentController(CibPayService cibPayService, IOrderService orderService) : Controller
{
    private readonly CibPayService _cibPayService = cibPayService;
    private readonly IOrderService _orderService = orderService;

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

        var result = await _orderService.CreateFinishedOrderAsync(order_id);

        if (!result)
        {
            var rejectModel = new RejectViewModel("Something went wrong");

            return View("Reject", rejectModel);
        }

        var successModel = new SuccessViewModel(paymentResult.Data.amount);

        return View("Success", successModel);
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AmountDto amount)
    {
        return Ok(await _orderService.CreateOrderAsync(amount.Amount));
    }

    public class AmountDto
    {
        public decimal Amount { get; set; }
    }

}
