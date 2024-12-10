namespace PaymentService.CibPayIntegration.Models.GetOrder.Responses;

public class Cashflow
{
    public string incoming { get; set; }
    public string fee { get; set; }
    public string receivable { get; set; }
    public string amount { get; set; }
    public string currency { get; set; }
    public string reserve { get; set; }
}
