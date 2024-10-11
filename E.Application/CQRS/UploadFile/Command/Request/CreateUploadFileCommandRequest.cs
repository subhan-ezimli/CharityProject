using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.UploadFile.Command.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E.Application.CQRS.UploadFile.Command.Request;

public class CreateUploadFileCommandRequest : IRequest<TypedResponseModel<CreateUploadFileCommandResponse>>
{
    public IFormFile File { get; set; }
}
