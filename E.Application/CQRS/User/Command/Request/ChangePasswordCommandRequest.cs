using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Command.Request;

public class ChangePasswordCommandRequest : IRequest<TypedResponseModel<ChangePasswordCommandResponse>>
{

}
