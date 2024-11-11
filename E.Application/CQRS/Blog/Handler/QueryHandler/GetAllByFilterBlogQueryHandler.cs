using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Query.Request;
using E.Application.CQRS.Blog.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.CQRS.Blog.Handler.QueryHandler;


public class GetAllByFilterBlogQueryHandler : IRequestHandler<GetAllByFilterBlogQueryRequest, ResponseModelPagination<GetAllByFilterBlogQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllByFilterBlogQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModelPagination<GetAllByFilterBlogQueryResponse>> Handle(GetAllByFilterBlogQueryRequest request, CancellationToken cancellationToken)
    {
        var datasBefore = _unitOfWork.BlogRepository.GetAll();

        var datas = datasBefore.Skip(request.Limit * (request.Page - 1)).Take(request.Limit);

        var list = new List<GetAllByFilterBlogQueryResponse>();

        foreach (var data in datas)
        {
            var blog = new GetAllByFilterBlogQueryResponse()
            {
                Content = data.Content,
                CreatedDate = data.CreatedDate,
                Header = data.Header,
                Id = data.Id,
                FileUrl = $"http://localhost:5245/api/UploadFile/download/{data.UploadFileId}",
                UploadFileId = data.UploadFileId
            };
            list.Add(blog);
        }

        var pagination = new Pagination<GetAllByFilterBlogQueryResponse>()
        {
            Datas = list,
            TotalDataCount = await datasBefore.CountAsync()
        };

        return new ResponseModelPagination<GetAllByFilterBlogQueryResponse>()
        {
            Data = pagination
        };
    }
}
