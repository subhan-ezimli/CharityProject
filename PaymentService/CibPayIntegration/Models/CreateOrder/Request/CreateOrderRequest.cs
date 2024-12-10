namespace PaymentService.CibPayIntegration.Models.CreateOrder.Request;

public class CreateOrderRequest
{
    public double Amount { get; set; }
    public string Currency { get; set; }
    public ExtraFields ExtraFields { get; set; }
    public string MerchantOrderId { get; set; }
    public Options Options { get; set; }
    public ClientForCreate Client { get; set; }
}
