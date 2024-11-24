using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Command.Request;

public class DeleteUserCommandRequest : IRequest<TypedResponseModel<DeleteUserCommandResponse>>
{
    public int Id { get; set; }
}
