namespace PaymentService.CibPayIntegration.Models.CreateOrder.Request;

public class Options
{
    public bool AutoCharge { get; set; }
    public string ExpirationTimeout { get; set; }
    public int Force3d { get; set; }
    public string Language { get; set; }
    public string ReturnUrl { get; set; }
}
