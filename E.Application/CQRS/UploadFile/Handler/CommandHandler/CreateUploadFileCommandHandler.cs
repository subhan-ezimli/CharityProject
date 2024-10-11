using A.Domain.Entities;
using B.Repository.Common;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using D.Dal.SqlServer.Context;
using E.Application.CQRS.UploadFile.Command.Request;
using E.Application.CQRS.UploadFile.Command.Response;
using E.Application.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace E.Application.CQRS.UploadFile.Handler.CommandHandler;

public class CreateUploadFileCommandHandler : IRequestHandler<CreateUploadFileCommandRequest, TypedResponseModel<CreateUploadFileCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    public CreateUploadFileCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<TypedResponseModel<CreateUploadFileCommandResponse>> Handle(CreateUploadFileCommandRequest request, CancellationToken cancellationToken)
    {

        var currentDate = DateTime.Now;
        var path = _configuration.GetSection("Data:BasePath:AppFilePhotos").Value;

        var formFile = request.File;

        path = CreateFolder.CreateDirectoryForFile(path!, currentDate);
        string guid = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

        var fullPath = Path.Combine(path, guid);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream, cancellationToken);
        }

        var uploadedFileEntity = new A.Domain.Entities.UploadFile()
        {
            FileNameOnDisk = guid,
            FilePath = fullPath,
            FileSize = formFile.Length,
            MimeType = formFile.ContentType.ToString(),
            OriginalFileName = formFile.FileName,
            UploadedDate = DateTime.Now
        };

        await _unitOfWork.UploadFileRepository.AddAsync(uploadedFileEntity);
        await _unitOfWork.SaveChanges();

        return new TypedResponseModel<CreateUploadFileCommandResponse>
        {
            Data = new CreateUploadFileCommandResponse
            {
                Id = uploadedFileEntity.Id,
                CreatedDate = currentDate,
                FileSize = formFile.Length,
                MimeType = formFile.ContentType.ToString(),
                Name = formFile.FileName,

            }
        };

    }
}
