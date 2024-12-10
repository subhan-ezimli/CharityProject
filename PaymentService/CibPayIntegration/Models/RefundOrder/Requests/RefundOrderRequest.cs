namespace PaymentService.CibPayIntegration.Models.RefundOrder.Requests
{
    public class RefundOrderRequest
    {
        public string OrderId { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
