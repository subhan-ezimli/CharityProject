namespace PaymentService.CibPayIntegration.Models.GetOrder.Responses;

public class Operation
{
    public string type { get; set; }
    public string iso_message { get; set; }
    public string created { get; set; }
    public string amount { get; set; }
    public string auth_code { get; set; }
    public string iso_response_code { get; set; }
    public Cashflow cashflow { get; set; }
    public string currency { get; set; }
    public string status { get; set; }
    public string arn { get; set; }
}
