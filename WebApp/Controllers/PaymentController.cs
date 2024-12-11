using PaymentService.CibPayIntegration.Implementations;

namespace WebApp.Controllers;

public class PaymentController(CibPayService cibPayService) : BaseController
{
    private readonly CibPayService _cibPayService = cibPayService;
}
