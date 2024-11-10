using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Volunteer.Command.Response;
using MediatR;

namespace E.Application.CQRS.Volunteer.Command.Request
{
    public class UpdateVolunteerCommandRequest : IRequest<TypedResponseModel<UpdateVolunteerCommandResponse>>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int UploadFileId { get; set; }
    }

}
