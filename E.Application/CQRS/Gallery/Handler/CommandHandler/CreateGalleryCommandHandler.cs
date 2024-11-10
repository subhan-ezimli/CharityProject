using A.Domain.Entities;
using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Response;
using E.Application.CQRS.Gallery.Command.Request;
using E.Application.CQRS.Gallery.Command.Response;
using MediatR;

namespace E.Application.CQRS.Gallery.Handler.CommandHandler;

public class CreateGalleryCommandHandler : IRequestHandler<CreateGalleryCommandRequest, TypedResponseModel<CreateGalleryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateGalleryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<CreateGalleryCommandResponse>> Handle(CreateGalleryCommandRequest request, CancellationToken cancellationToken)
    {
        var gallery = new A.Domain.Entities.Gallery();
        gallery.UploadFileId = request.UploadFileId;
        await _unitOfWork.GalleryRepository.AddAsync(gallery, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<CreateGalleryCommandResponse>
        {
            Data = new CreateGalleryCommandResponse
            {
                Message = "Successfully added"
            }
        };

    }
}
