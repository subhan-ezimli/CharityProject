namespace PaymentService.CibPayIntegration.Models.GetOrder.Responses;

public class GetPaymentResponse
{
    public string failure_message { get; set; }
    public decimal amount { get; set; }
    public decimal amount_charged { get; set; }
    public decimal amount_refunded { get; set; }
    public string auth_code { get; set; }
    public Card card { get; set; }
    public Client client { get; set; }
    public DateTime created { get; set; }
    public string currency { get; set; }
    public Dictionary<string, object> custom_fields { get; set; }
    public string description { get; set; }
    public string descriptor { get; set; }
    public string id { get; set; }
    public Issuer issuer { get; set; }
    public Location location { get; set; }
    public string merchant_order_id { get; set; }
    public List<Operation> operations { get; set; }
    public string pan { get; set; }
    public Dictionary<string, object> secure3d { get; set; }
    public string segment { get; set; }
    public string status { get; set; }
    public DateTime? updated { get; set; }
}
