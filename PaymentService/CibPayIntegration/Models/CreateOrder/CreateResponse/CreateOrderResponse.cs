namespace PaymentService.CibPayIntegration.Models.CreateOrder.CreateResponse;

public class CreateOrderResponse
{
    public string OrderId { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public string PaymentLink { get; set; }


    public CreateOrderResponse(bool succeeded, string message)
    {
        Succeeded = succeeded;
        Message = message;
        PaymentLink = null!;
    }
 

     public CreateOrderResponse(string paymentLink, string id)
    {
        Succeeded = true;
        Message = null!;
        PaymentLink = paymentLink;
        OrderId = id;
    }

}