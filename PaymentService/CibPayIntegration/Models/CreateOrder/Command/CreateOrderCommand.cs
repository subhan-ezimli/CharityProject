namespace PaymentService.CibPayIntegration.Models.CreateOrder.Command
{
    public class CreateOrderCommand
    {
        public decimal Amount { get; set; }
        public string OrderAmount { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

    }
}
