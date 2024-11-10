using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Gallery.Command.Response;
using MediatR;

namespace E.Application.CQRS.Gallery.Command.Request;

public class DeleteGalleryCommandRequest : IRequest<TypedResponseModel<DeleteGalleryCommandResponse>>
{
    public int Id { get; set; }

    public DeleteGalleryCommandRequest(int id)
    {
        Id = id;
    }
}