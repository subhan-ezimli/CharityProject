using A.Domain.Enums;

namespace E.Application.CQRS.User.Query.Response
{
    public class UserGetAllByFilterQueryResponse
    {
        public int Id { get; set; }
        public int UserRole { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
