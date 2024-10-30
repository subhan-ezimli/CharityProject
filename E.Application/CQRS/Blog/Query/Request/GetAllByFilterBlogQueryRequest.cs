using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Query.Response;
using MediatR;

namespace E.Application.CQRS.Blog.Query.Request;

public class GetAllByFilterBlogQueryRequest : IRequest<ResponseModelPagination<GetAllByFilterBlogQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}