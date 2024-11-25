using A.Domain.Enums;
using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using E.Application.Security;
using MediatR;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, TypedResponseModel<DeleteUserCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<TypedResponseModel<DeleteUserCommandResponse>> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(_userContext.MustGetUserId());
        if (currentUser.UserRole != (int)UserRole.Admin)
        {
            throw new PermissionDeniedErrorException();
        }
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user.Isdeleted || user == null)
        {
            throw new BadRequestException("user not found");
        }
        await _unitOfWork.UserRepository.DeleteAsync(user, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<DeleteUserCommandResponse>()
        {
            Data = new DeleteUserCommandResponse()
            {
                Message = "successfully deleted"
            }
        };
    }
}
