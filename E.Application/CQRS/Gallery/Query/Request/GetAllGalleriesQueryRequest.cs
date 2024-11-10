using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Gallery.Query.Response;
using MediatR;

namespace E.Application.CQRS.Gallery.Query.Request;

public class GetAllGalleriesQueryRequest : IRequest<ResponseModelPagination<GetALlGalleriesQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}
