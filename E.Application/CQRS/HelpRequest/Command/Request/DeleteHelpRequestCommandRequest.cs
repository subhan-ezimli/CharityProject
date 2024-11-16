using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Command.Response;
using MediatR;

namespace E.Application.CQRS.HelpRequest.Command.Request;

public class DeleteHelpRequestCommandRequest : IRequest<TypedResponseModel<DeleteHelpRequestCommandResponse>>
{
    public int Id { get; set; }

    public DeleteHelpRequestCommandRequest(int id)
    {
        Id = id;
    }
}
