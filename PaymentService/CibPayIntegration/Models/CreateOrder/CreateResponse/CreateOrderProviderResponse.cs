using PaymentService.CibPayIntegration.Models.GetOrder.Responses;

namespace PaymentService.CibPayIntegration.Models.CreateOrder.CreateResponse;
public class CreateOrderProviderResponse
{
    public List<GetPaymentResponse> orders { get; set; }
}
