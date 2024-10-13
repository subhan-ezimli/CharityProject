using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Command.Request;
using E.Application.CQRS.HelpRequest.Command.Response;
using MediatR;

namespace E.Application.CQRS.HelpRequest.Handler;

public class CreateHelpRequestCommandHandler : IRequestHandler<CreateHelpRequestCommandRequest, TypedResponseModel<CreateHelpRequestCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateHelpRequestCommandHandler(IUnitOfWork unitOfwork)
    {
        _unitOfWork = unitOfwork;
    }

    public async Task<TypedResponseModel<CreateHelpRequestCommandResponse>> Handle(CreateHelpRequestCommandRequest request, CancellationToken cancellationToken)
    {

        var helpRequest = new A.Domain.Entities.HelpRequest()
        {
            Name = request.Name,
            Surname = request.Surname,
            FathersName = request.FathersName,
            Address = request.Address,
            RegionId = request.RegionId,
            PhoneNumber = request.PhoneNumber,
            ShortInfo = request.ShortInfo

        };

        await _unitOfWork.HelpRequestRepository.AddAsync(helpRequest, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<CreateHelpRequestCommandResponse>
        {
            Data = new CreateHelpRequestCommandResponse
            {
                Message = "Successfully added"
            }
        };

    }
}

