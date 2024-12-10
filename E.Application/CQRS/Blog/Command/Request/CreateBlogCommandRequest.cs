using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Response;
using MediatR;

namespace E.Application.CQRS.Blog.Command.Request;

public class CreateBlogCommandRequest : IRequest<TypedResponseModel<CreateBlogCommandResponse>>
{
    public string Header { get; set; }
    public int UploadFileId { get; set; }
    public string Content { get; set; }
}


//test