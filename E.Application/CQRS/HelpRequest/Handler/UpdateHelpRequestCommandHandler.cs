using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Command.Request;
using E.Application.CQRS.HelpRequest.Command.Response;
using MediatR;

namespace E.Application.CQRS.HelpRequest.Handler;

public class UpdateHelpRequestCommandHandler : IRequestHandler<UpdateHelpRequestCommandRequest, TypedResponseModel<UpdateHelpRequestCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHelpRequestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<UpdateHelpRequestCommandResponse>> Handle(UpdateHelpRequestCommandRequest request, CancellationToken cancellationToken)
    {

        var helpRequest = await _unitOfWork.HelpRequestRepository.GetByIdAsync(request.Id, cancellationToken);

        if (helpRequest == null || helpRequest.IsDeleted)
        {
            throw new BadRequestException("Not Found");
        }


        helpRequest.Name = request.Name;
        helpRequest.Surname = request.Surname;
        helpRequest.FathersName = request.FathersName;
        helpRequest.Address = request.Address;
        helpRequest.RegionId = request.RegionId;
        helpRequest.PhoneNumber = request.PhoneNumber;
        helpRequest.ShortInfo = request.ShortInfo;



        await _unitOfWork.HelpRequestRepository.UpdateAsync(helpRequest);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<UpdateHelpRequestCommandResponse>
        {
            Data = new UpdateHelpRequestCommandResponse
            {
                Message = "Successfully updated"
            }
        };

    }
}
