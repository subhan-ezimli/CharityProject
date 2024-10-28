using B.Repository.Common;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Query.Request;
using E.Application.CQRS.HelpRequest.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace E.Application.CQRS.HelpRequest.Handler;

public class GetAllByFilterHelpRequestQueryHandler : IRequestHandler<GetAllByFilterHelpRequestQueryRequest, ResponseModelPagination<GetAllByFilterHelpRequestQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllByFilterHelpRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModelPagination<GetAllByFilterHelpRequestQueryResponse>> Handle(GetAllByFilterHelpRequestQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = _unitOfWork.HelpRequestRepository.GetAllAsQueryable();
        datas.WhereIf(request.Name, x => x.Name.ToLower().Contains(request.Name.ToLower()))
             .WhereIf(request.Surname, x => x.Surname.ToLower().Contains(request.Surname.ToLower()))
             .WhereIf(request.FathersName, x => x.FathersName.ToLower().Contains(request.FathersName.ToLower()))
             .WhereIf(request.PhoneNumber, x => x.PhoneNumber.Contains(request.PhoneNumber));

        datas = datas.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        int datasCount = await datas.CountAsync();


        var dataList = new List<GetAllByFilterHelpRequestQueryResponse>();
        foreach (var item in datas)
        {
            var getallbyFilterHelpRequest = new GetAllByFilterHelpRequestQueryResponse()
            {
                Address = item.Address,
                FathersName = item.FathersName,
                Surname = item.Surname,
                Name = item.Name,
                PhoneNumber = item.PhoneNumber,
                ShortInfo = item.ShortInfo,
                CreatedDate = item.CreatedDate,
                region = new DTOs.RegionDto()
                {
                    Id = item.RegionId,
                    Name = item.Name,
                },
                Id = item.Id
            };

            dataList.Add(getallbyFilterHelpRequest);
        }

        var pagination = new Pagination<GetAllByFilterHelpRequestQueryResponse>
        {
            Datas = dataList,
            TotalDataCount = datasCount,
            // IsSuccess = true
        };

        return new ResponseModelPagination<GetAllByFilterHelpRequestQueryResponse>()
        {
            Data = pagination,
            IsSuccess = true
        };

    }
}
