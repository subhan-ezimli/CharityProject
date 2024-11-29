namespace A.Domain.Entities
{
    public class PendingPayment
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        //public Guid UserId { get; set; }
        //public Guid BookId { get; set; }
        public string Discriminator { get; set; }


        //Navigation Properties
        //public User User { get; set; }
        //public Book Book { get; set; }
    }
}
