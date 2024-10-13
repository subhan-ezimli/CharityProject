using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Query.Response;
using MediatR;

namespace E.Application.CQRS.Project.Query.Request;

public class GetAllByFilterProjectQueryRequest : IRequest<ResponseModelPagination<GetAllByFilterProjectQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;

    public string? Header { get; set; }
}
