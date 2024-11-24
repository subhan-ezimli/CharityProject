using A.Domain.Enums;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Query.Response;
using MediatR;

namespace E.Application.CQRS.User.Query.Request
{
    public class UserGetAllByFilterQueryRequest : IRequest<ResponseModelPagination<UserGetAllByFilterQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FathersName { get; set; }
        public string? Email { get; set; }
    }
}
