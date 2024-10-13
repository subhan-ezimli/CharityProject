using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Query.Response;
using MediatR;

namespace E.Application.CQRS.HelpRequest.Query.Request;

public class GetAllByFilterHelpRequestQueryRequest : IRequest<ResponseModelPagination<GetAllByFilterHelpRequestQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? FathersName { get; set; }
    public string? PhoneNumber { get; set; }

}
