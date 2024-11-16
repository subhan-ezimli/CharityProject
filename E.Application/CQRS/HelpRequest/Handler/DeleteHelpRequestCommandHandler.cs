using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Command.Request;
using E.Application.CQRS.HelpRequest.Command.Response;
using MediatR;

namespace E.Application.CQRS.HelpRequest.Handler;

public class DeleteHelpRequestCommandHandler : IRequestHandler<DeleteHelpRequestCommandRequest, TypedResponseModel<DeleteHelpRequestCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHelpRequestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<DeleteHelpRequestCommandResponse>> Handle(DeleteHelpRequestCommandRequest request, CancellationToken cancellationToken)
    {
        var helpRequest = await _unitOfWork.HelpRequestRepository.GetByIdAsync(request.Id, cancellationToken);
        if (helpRequest == null || helpRequest.IsDeleted)
        {
            throw new BadRequestException("Not found");
        }

        await _unitOfWork.HelpRequestRepository.DeleteAsync(helpRequest, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<DeleteHelpRequestCommandResponse>
        {
            Data = new DeleteHelpRequestCommandResponse
            {
                Message = "Succesfully deleted"
            }
        };
    }
}
