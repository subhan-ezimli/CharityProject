namespace WebApp.View.ViewModels; 

public class SuccessViewModel(decimal amount)
{
    public decimal Amount { get; set; } = amount;
}
