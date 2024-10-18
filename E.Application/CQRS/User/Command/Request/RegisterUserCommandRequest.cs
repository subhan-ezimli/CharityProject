using A.Domain.Enums;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Command.Request
{
    public class RegisterUserCommandRequest : IRequest<TypedResponseModel<RegisterUserCommandResponse>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public string Email { get; set; }
    }
}
