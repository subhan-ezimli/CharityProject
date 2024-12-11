namespace A.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public int PendingPaymentId { get; set; }
}
