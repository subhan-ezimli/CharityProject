using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Command.Response;
using MediatR;

namespace E.Application.CQRS.Project.Command.Request;

public class UpdateProjectCommandRequest : IRequest<TypedResponseModel<UpdateProjectCommandResponse>>
{
    public int Id { get; set; }

    public string Header { get; set; }
    public int UploadFileId { get; set; }
    public string Content { get; set; }
}
