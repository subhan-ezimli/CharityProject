namespace A.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid PendingPaymentId { get; set; }

    public PendingPayment PendingPayment { get; set; }
}
