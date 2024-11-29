namespace WebApp.Views.ViewModels
{
    public class RejectViewModel(string message)
    {
        public string ErrorMessage { get; set; } = message;
    }
}
