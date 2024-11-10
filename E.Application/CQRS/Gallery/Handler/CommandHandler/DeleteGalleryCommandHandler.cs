using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Gallery.Command.Request;
using E.Application.CQRS.Gallery.Command.Response;
using MediatR;
using Microsoft.Identity.Client.Extensibility;

namespace E.Application.CQRS.Gallery.Handler.CommandHandler
{
    public class DeleteGalleryCommandHandler : IRequestHandler<DeleteGalleryCommandRequest, TypedResponseModel<DeleteGalleryCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGalleryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TypedResponseModel<DeleteGalleryCommandResponse>> Handle(DeleteGalleryCommandRequest request, CancellationToken cancellationToken)
        {
            var gallery = await _unitOfWork.GalleryRepository.GetByIdAsync(request.Id, cancellationToken);
            if (gallery == null)
            {
                throw new BadRequestException("not found");
            }
            await _unitOfWork.GalleryRepository.DeleteAsync(gallery, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            return new TypedResponseModel<DeleteGalleryCommandResponse>()
            {
                Data = new DeleteGalleryCommandResponse
                {
                    Message = "Successfully deleted"
                }
            };
        }

    }
}
