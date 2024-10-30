using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Response;
using MediatR;

namespace E.Application.CQRS.Blog.Command.Request;

public class DeleteBlogCommandRequest:IRequest<TypedResponseModel<DeleteBlogCommandResponse>>
{
    public int Id { get; set; }
}
