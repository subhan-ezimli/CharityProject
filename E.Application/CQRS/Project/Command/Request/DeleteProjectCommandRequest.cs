using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Command.Response;
using MediatR;

namespace E.Application.CQRS.Project.Command.Request;

public class DeleteProjectCommandRequest : IRequest<TypedResponseModel<DeleteProjectCommandResponse>>
{
    public int Id { get; set; }
}
