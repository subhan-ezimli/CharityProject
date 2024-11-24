using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using MediatR;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, TypedResponseModel<DeleteUserCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public async Task<TypedResponseModel<DeleteUserCommandResponse>> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken);
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
