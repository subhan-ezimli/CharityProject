using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, TypedResponseModel<UpdateUserCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<UpdateUserCommandResponse>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken);

        var checkEmail = await _unitOfWork.UserRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (user != null || checkEmail.Id != user.Id)
        {
            throw new BadRequestException("Email is allready exist");
        }

        user.FathersName = request.FathersName;
        user.Isdeleted = false;
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.UserRole = request.UserRole;
        user.Email = request.Email;

        var hashedPasswordTuple = PasswordHasher.ComputeStringToSha256Hash(request.Password);
        user.PasswordHash = hashedPasswordTuple;

        await _unitOfWork.UserRepository.UpdateAsync(user);

        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<UpdateUserCommandResponse>()
        {
            Data = new UpdateUserCommandResponse
            {
                Message = "Successfully updated"
            }
        };
    }
}
