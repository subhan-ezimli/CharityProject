using A.Domain.Entities;
using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using E.Application.Security;
using MediatR;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, TypedResponseModel<ChangePasswordCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<TypedResponseModel<ChangePasswordCommandResponse>> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(_userContext.MustGetUserId());

        if (!PasswordHasher.VerifyPasswordHash(request.CurrentPassword, currentUser.PasswordHash))
        {
            throw new BadRequestException("Old password is wrong");
        }

        currentUser.PasswordHash = PasswordHasher.ComputeStringToSha256Hash(request.NewPassword);
        await _unitOfWork.UserRepository.UpdateAsync(currentUser);

        await _unitOfWork.SaveChanges(cancellationToken);
        return new TypedResponseModel<ChangePasswordCommandResponse>()
        {
            Data = new ChangePasswordCommandResponse
            {
                Message = "successfully changed"
            }
        };
    }
}
