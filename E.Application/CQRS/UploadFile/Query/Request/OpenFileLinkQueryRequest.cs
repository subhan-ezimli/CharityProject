using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.UploadFile.Query.Response;
using MediatR;

namespace E.Application.CQRS.UploadFile.Query.Request;



public class OpenFileLinkQueryRequest : IRequest<TypedResponseModel<OpenFileLinkQueryResponse>>
{
    public int Id { get; set; }

    public OpenFileLinkQueryRequest(int id)
    {
        Id = id;
    }
}
