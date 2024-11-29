namespace PaymentService.CibPayIntegration.Models.BaseResponse;

public class PaymentLinkResponse
{
    public string PaymentLink { get; set; }

    public PaymentLinkResponse()
    {
        PaymentLink = null!;
    }

    public PaymentLinkResponse(string paymentLink)
    {
        PaymentLink = paymentLink;
    }
}