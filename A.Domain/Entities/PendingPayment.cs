namespace A.Domain.Entities;

public class PendingPayment
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid UserId { get; set; }
    public string Discriminator { get; set; }
}
