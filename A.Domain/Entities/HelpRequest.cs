namespace A.Domain.Entities;

public class HelpRequest
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }

    public string PhoneNumber { get; set; }
    public int RegionId { get; set; }

    public string Address { get; set; }
    public string ShortInfo { get; set; }
}