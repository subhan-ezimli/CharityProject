using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Command.Request;

public class UpdateMeCommandRequest : IRequest<TypedResponseModel<UpdateMeCommandResponse>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }

    public string Email { get; set; }
}
