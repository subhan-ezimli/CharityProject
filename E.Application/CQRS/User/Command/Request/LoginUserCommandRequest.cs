using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Command.Request;

public class LoginUserCommandRequest : IRequest<TypedResponseModel<LoginUserCommandResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
