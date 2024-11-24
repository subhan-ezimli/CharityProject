using A.Domain.Entities;
using A.Domain.Enums;
using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, TypedResponseModel<RegisterUserCommandResponse>>
{
    private readonly IUnitOfWork _unitOfwork;
    public RegisterUserCommandHandler(IUnitOfWork unitOfwork)
    {
        _unitOfwork = unitOfwork;
    }
    public async Task<TypedResponseModel<RegisterUserCommandResponse>> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var checkUser = await _unitOfwork.UserRepository.FindByEmailAsync(request.Email, cancellationToken);
        if (checkUser != null)
        {
            throw new BadRequestException("email is allready exist");
        }

        var user = new A.Domain.Entities.User();

        user.FathersName = request.FathersName;
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.BirthDate = DateTime.Now;
        user.UserRole = request.UserRole;
        user.Email = request.Email;

        var hashedPasswordTuple = PasswordHasher.ComputeStringToSha256Hash(request.Password);
        user.PasswordHash = hashedPasswordTuple;

        await _unitOfwork.UserRepository.AddAsync(user, cancellationToken);

        await _unitOfwork.SaveChanges(cancellationToken);

        return new TypedResponseModel<RegisterUserCommandResponse>()
        {
            Data = new RegisterUserCommandResponse
            {
                Message = "Successfully added"
            }
        };
    }
}
