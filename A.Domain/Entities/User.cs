using A.Domain.BaseEntities;

namespace A.Domain.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool Isdeleted { get; set; } = false;
    public DateTime BirthDate { get; set; }

}
