namespace PaymentService.CibPayIntegration.Models.GetOrder.Requests;
public class GetOrdersRequest
{
    public string Status { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public string MerchantOrderId { get; set; }
    public string CardType { get; set; }
    public string CardSubtype { get; set; }
    public string IpAddress { get; set; }
    public string Expand { get; set; }
}
