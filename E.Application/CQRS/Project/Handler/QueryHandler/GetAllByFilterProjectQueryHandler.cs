using B.Repository.Common;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Project.Query.Request;
using E.Application.CQRS.Project.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.CQRS.Project.Handler.QueryHandler;

public class GetAllByFilterProjectQueryHandler : IRequestHandler<GetAllByFilterProjectQueryRequest, ResponseModelPagination<GetAllByFilterProjectQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllByFilterProjectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModelPagination<GetAllByFilterProjectQueryResponse>> Handle(GetAllByFilterProjectQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = _unitOfWork.ProjectRepository.GetAllAsQueryable();
        datas = datas.WhereIf(request.Header, x => x.Header.ToLower().Contains(request.Header.ToLower()));

        datas = datas.Skip(request.Limit * (request.Page - 1)).Take(request.Limit);

        var list = new List<GetAllByFilterProjectQueryResponse>();
       
        foreach (var data in datas)
        {
            var project = new GetAllByFilterProjectQueryResponse()
            {
                Content = data.Content,
                CreatedDate = data.CreatedDate,
                Header = data.Header,
                Id = data.Id,
                FileUrl = $"https://localhost:44366/api/UploadFile/download/{data.UploadFileId}"
            };
            list.Add(project);
        }

        var pagination = new Pagination<GetAllByFilterProjectQueryResponse>()
        {
            Datas = list,
            TotalDataCount = await datas.CountAsync()
        };


        return new ResponseModelPagination<GetAllByFilterProjectQueryResponse>()
        {
            Data = pagination
        };
    }
}
