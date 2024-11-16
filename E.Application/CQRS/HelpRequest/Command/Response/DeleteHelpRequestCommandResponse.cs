using MediatR;

namespace E.Application.CQRS.HelpRequest.Command.Response;

public class DeleteHelpRequestCommandResponse : IRequest<DeleteHelpRequestCommandResponse>
{
    public string Message { get; set; }
}
