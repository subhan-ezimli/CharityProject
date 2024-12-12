namespace A.Domain.Entities;

public class PendingPayment
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
    public string Discriminator { get; set; }
}
