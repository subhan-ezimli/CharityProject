using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Query.Response;
using MediatR;

namespace E.Application.CQRS.User.Query.Request;

public class SessionUserQueryRequest : IRequest<TypedResponseModel<SessionUserQueryResponse>>
{
}
