using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Volunteer.Command.Response;
using MediatR;

namespace E.Application.CQRS.Volunteer.Command.Request
{
    public class DeleteVolunteerCommandRequest:IRequest<TypedResponseModel<DeleteVolunteerCommandResponse>>
    {
        public int Id { get; set; }

        public DeleteVolunteerCommandRequest(int id)
        {
            Id = id;
        }
    }
}
