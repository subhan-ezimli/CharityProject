using C.Common.GlobalResponses.Generics;
using MediatR;

namespace E.Application.CQRS.User.Query.Response;

public class SessionUserQueryResponse : IRequest<TypedResponseModel<SessionUserQueryResponse>>
{
    public int Id { get; set; }

    public int UserRole { get; set; }
    public string UserRoleLabel { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }

    public string Email { get; set; }


}
