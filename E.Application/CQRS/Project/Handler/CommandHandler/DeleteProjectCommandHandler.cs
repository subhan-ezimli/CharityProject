using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Command.Request;
using E.Application.CQRS.Project.Command.Response;
using MediatR;

namespace E.Application.CQRS.Project.Handler.CommandHandler;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommandRequest, TypedResponseModel<DeleteProjectCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<DeleteProjectCommandResponse>> Handle(DeleteProjectCommandRequest request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
        {
            throw new BadRequestException("not Found");
        }

        await _unitOfWork.ProjectRepository.DeleteAsync(project, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return new TypedResponseModel<DeleteProjectCommandResponse>
        {
            Data = new DeleteProjectCommandResponse
            {
                Message = "Succesfully deleted"
            }
        };
    }

}
