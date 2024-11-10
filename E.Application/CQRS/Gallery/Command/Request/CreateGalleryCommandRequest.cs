using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Gallery.Command.Response;
using MediatR;

namespace E.Application.CQRS.Gallery.Command.Request;

public class CreateGalleryCommandRequest : IRequest<TypedResponseModel<CreateGalleryCommandResponse>>
{
    public int UploadFileId { get; set; }
}