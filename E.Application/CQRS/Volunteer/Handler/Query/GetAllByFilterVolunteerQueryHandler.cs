using B.Repository.Common;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Volunteer.Query.Request;
using E.Application.CQRS.Volunteer.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.CQRS.Volunteer.Handler.Query;

public class GetAllByFilterVolunteerQueryHandler : IRequestHandler<GetAllByFilterVolunteerQueryRequest, ResponseModelPagination<GetAllByFilterVolunteerQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllByFilterVolunteerQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<ResponseModelPagination<GetAllByFilterVolunteerQueryResponse>> Handle(GetAllByFilterVolunteerQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = _unitOfWork.VolunteerRepository.GetAllAsQueryable();

        datas = datas.WhereIf(request.Name, x => x.Name.ToLower().Contains(request.Name.ToLower()))
                     .WhereIf(request.Surname, x => x.Surname.ToLower().Contains(request.Surname.ToLower()))
                     .WhereIf(request.FathersName, x => x.FathersName.ToLower().Contains(request.FathersName.ToLower()))
                     .WhereIf(request.PhoneNumber, x => x.PhoneNumber.Contains(request.PhoneNumber))
                     .WhereIf(request.Address, x => x.Address.ToLower().Contains(request.Address.ToLower()))
                     .WhereIf(request.Email, x => x.Email.ToLower().Contains(request.Email.ToLower()));

        var paginatedDatas = datas.Skip(request.Limit * (request.Page - 1)).Take(request.Limit);

        var responseDatas = new List<GetAllByFilterVolunteerQueryResponse>();

        foreach (var item in paginatedDatas)
        {
            var data = new GetAllByFilterVolunteerQueryResponse()
            {
                Address = item.Address,
                BirthDate = item.BirthDate,
                CreatedDate = item.CreatedDate,
                Email = item.Email,
                FathersName = item.FathersName,
                PhoneNumber = item.PhoneNumber,
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                UploadFileId = item.UploadFileId,
                FileUrl = $"http://localhost:5245/api/UploadFile/download/{item.UploadFileId}"
            };
            responseDatas.Add(data);
        }

        var pagination = new Pagination<GetAllByFilterVolunteerQueryResponse>()
        {
            Datas = responseDatas,
            TotalDataCount = await datas.CountAsync()
        };

        var response = new ResponseModelPagination<GetAllByFilterVolunteerQueryResponse>()
        {
            Data = pagination
        };
        return response;
    }
}
