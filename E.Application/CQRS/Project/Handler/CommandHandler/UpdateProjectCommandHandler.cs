using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Command.Request;
using E.Application.CQRS.Project.Command.Response;
using MediatR;

namespace E.Application.CQRS.Project.Handler.CommandHandler;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommandRequest, TypedResponseModel<UpdateProjectCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<TypedResponseModel<UpdateProjectCommandResponse>> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
    {

        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(request.Id, cancellationToken);
        if (project == null && project.IsDeleted)
        {
            throw new BadRequestException("not found");
        }

        project.Header = request.Header;
        project.UploadFileId = request.UploadFileId;
        project.Content = request.Content;

        await _unitOfWork.ProjectRepository.UpdateAsync(project);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<UpdateProjectCommandResponse>
        {
            Data = new UpdateProjectCommandResponse
            {
                Message = "successfully updated"
            }
        };
    }
}
