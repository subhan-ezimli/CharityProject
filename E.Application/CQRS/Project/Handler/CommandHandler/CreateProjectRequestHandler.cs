using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Command.Request;
using E.Application.CQRS.Project.Command.Response;
using MediatR;

namespace E.Application.CQRS.Project.Handler.CommandHandler;

public class CreateProjectRequestHandler : IRequestHandler<CreateProjectCommandRequest, TypedResponseModel<CreateProjectCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateProjectRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<TypedResponseModel<CreateProjectCommandResponse>> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        var project = new A.Domain.Entities.Project();
        project.Header = request.Header;
        project.Content = request.Content;
        project.UploadFileId = request.UploadFileId;

        await _unitOfWork.ProjectRepository.AddAsync(project, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<CreateProjectCommandResponse>
        {
            Data = new CreateProjectCommandResponse
            {
                Message = "Successfully added"
            }
        };
    }
}