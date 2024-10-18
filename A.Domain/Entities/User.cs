using A.Domain.BaseEntities;
using A.Domain.Enums;

namespace A.Domain.Entities;
public class User : BaseEntity
{
    public int Id { get; set; }
    public bool Isdeleted { get; set; } = false;

    public string PasswordHash { get; set; }
    public UserRole UserRole { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }

    public string Email { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime BirthDate { get; set; }
}
