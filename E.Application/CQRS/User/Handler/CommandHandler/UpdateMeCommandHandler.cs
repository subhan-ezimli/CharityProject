using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Command.Response;
using E.Application.Security;
using MediatR;

namespace E.Application.CQRS.User.Handler.CommandHandler;

public class UpdateMeCommandHandler : IRequestHandler<UpdateMeCommandRequest, TypedResponseModel<UpdateMeCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    public UpdateMeCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<TypedResponseModel<UpdateMeCommandResponse>> Handle(UpdateMeCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(_userContext.MustGetUserId());
        if (user == null || user.Isdeleted)
        {
            throw new BadRequestException("user not found");
        }

        var checkEmail = await _unitOfWork.UserRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (checkEmail != null && checkEmail.Id != user.Id && !checkEmail.Isdeleted)
        {
            throw new BadRequestException("email is exist allready ");
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.Surname = request.Surname;
        user.FathersName = request.FathersName;

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<UpdateMeCommandResponse>
        {
            Data = new UpdateMeCommandResponse
            {
                Message = "successfully changed"
            }
        };
    }
}
