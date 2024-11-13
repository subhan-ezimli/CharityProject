using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Volunteer.Query.Response;
using MediatR;

namespace E.Application.CQRS.Volunteer.Query.Request;

public class GetAllByFilterVolunteerQueryRequest : IRequest<ResponseModelPagination<GetAllByFilterVolunteerQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? FathersName { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

}
