using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Query.Request;
using E.Application.CQRS.User.Query.Response;
using E.Application.Security;
using MediatR;

namespace E.Application.CQRS.User.Handler.QueryHandler;

public class SessionUserQueryHandler : IRequestHandler<SessionUserQueryRequest, TypedResponseModel<SessionUserQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    public SessionUserQueryHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<TypedResponseModel<SessionUserQueryResponse>> Handle(SessionUserQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(_userContext.MustGetUserId());

        if (user == null || user.Isdeleted)
        {
            throw new BadRequestException("user not found");
        }

        return new TypedResponseModel<SessionUserQueryResponse>
        {
            Data = new SessionUserQueryResponse
            {
                Email = user.Email,
                FathersName = user.FathersName,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserRole = user.UserRole,
                UserRoleLabel = (user.UserRole == 1) ? "Admin" : "Moderator"
            }
        };
    }
}
