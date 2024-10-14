using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.UploadFile.Query.Request;
using E.Application.CQRS.UploadFile.Query.Response;
using MediatR;

namespace E.Application.CQRS.UploadFile.Handler.QueryHandler;

public class OpenFileLinkQueryHandler : IRequestHandler<OpenFileLinkQueryRequest, TypedResponseModel<OpenFileLinkQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public OpenFileLinkQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<OpenFileLinkQueryResponse>> Handle(OpenFileLinkQueryRequest request, CancellationToken cancellationToken)
    {

        var currentFile = await _unitOfWork.UploadFileRepository.GetByIdAsync(request.Id, cancellationToken);

        if (currentFile == null)
        {
            return new TypedResponseModel<OpenFileLinkQueryResponse>
            {
                Data = null,
                Errors = new List<string> { "File not found." },
                IsSuccess = false
            };
        }


        return new TypedResponseModel<OpenFileLinkQueryResponse>
        {
            Data = new OpenFileLinkQueryResponse
            {
                FilePath = currentFile.FilePath,
                MimeType = currentFile.MimeType
            },
            IsSuccess = true
        };
    }
}
