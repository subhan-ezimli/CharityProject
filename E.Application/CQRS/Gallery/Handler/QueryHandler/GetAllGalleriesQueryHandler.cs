using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Gallery.Query.Request;
using E.Application.CQRS.Gallery.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography;

namespace E.Application.CQRS.Gallery.Handler.QueryHandler;

public class GetAllGalleriesQueryHandler : IRequestHandler<GetAllGalleriesQueryRequest, ResponseModelPagination<GetALlGalleriesQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGalleriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<ResponseModelPagination<GetALlGalleriesQueryResponse>> Handle(GetAllGalleriesQueryRequest request, CancellationToken cancellationToken)
    {

        var datas = _unitOfWork.GalleryRepository.GetAllAsQueryable();

        var paginatedDatas = datas.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        var list = new List<GetALlGalleriesQueryResponse>();

        foreach (var item in paginatedDatas)
        {
            var data = new GetALlGalleriesQueryResponse()
            {
                Id = item.Id,
                CreatedDate = item.CreatedDate,
                FileUrl = $"http://localhost:5245/api/UploadFile/download/{item.UploadFileId}",
            };
            list.Add(data);
        }

        var response = new ResponseModelPagination<GetALlGalleriesQueryResponse>();
        var pagination = new Pagination<GetALlGalleriesQueryResponse>()
        {
            Datas = list,
            TotalDataCount = await datas.CountAsync()
        };

        response.Data = pagination;
        return response;
    }
}
