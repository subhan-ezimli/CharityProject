using A.Domain.ViewModels;
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
        List<City> GetAllCities = new List<City>()
        {
            new City(1, "Bakı"),
            new City(2, "Sumqayıt"),
            new City(3, "Gəncə"),
            new City(4, "Mingəçevir"),
            new City(5, "Şirvan"),
            new City(6, "Lənkəran"),
            new City(7, "Şəki"),
            new City(8, "Yevlax"),
            new City(9, "Naxçıvan"),
            new City(10, "Quba"),
            new City(11, "Zaqatala"),
            new City(12, "Şamaxı"),
            new City(13, "Xırdalan"),
            new City(14, "Qəbələ"),
            new City(15, "Qazax"),
            new City(16, "Masallı"),
            new City(17, "Cəlilabad"),
            new City(18, "İsmayıllı"),
            new City(19, "Astara"),
            new City(20, "Tərtər"),
            new City(21, "Balakən"),
            new City(22, "Beyləqan"),
            new City(23, "Füzuli"),
            new City(24, "Saatlı"),
            new City(25, "Sabirabad"),
            new City(26, "Biləsuvar"),
            new City(27, "Hacıqabul"),
            new City(28, "Naftalan"),
            new City(29, "Ucar"),
            new City(30, "Ağdam"),
            new City(31, "Ağcabədi"),
            new City(32, "Ağdaş"),
            new City(33, "Bərdə"),
            new City(34, "Qobustan"),
            new City(35, "Xaçmaz"),
            new City(36, "Şəmkir"),
            new City(37, "Goranboy"),
            new City(38, "Qusar"),
            new City(39, "Salyan"),
            new City(40, "Lerik"),
            new City(41, "Zərdab"),
            new City(42, "Şuşa"),
            new City(43, "Laçın"),
            new City(44, "Kəlbəcər"),
            new City(45, "Qubadlı"),
            new City(46, "Cəbrayıl"),
            new City(47, "Zəngilan"),
            new City(48, "Xocalı"),
            new City(49, "Xocavənd"),
            new City(50, "Ağstafa")
        };


        var datas = _unitOfWork.HelpRequestRepository.GetAllAsQueryable();
        datas.WhereIf(request.Name, x => x.Name.ToLower().Contains(request.Name.ToLower()))
             .WhereIf(request.Surname, x => x.Surname.ToLower().Contains(request.Surname.ToLower()))
             .WhereIf(request.FathersName, x => x.FathersName.ToLower().Contains(request.FathersName.ToLower()))
             .WhereIf(request.PhoneNumber, x => x.PhoneNumber.Contains(request.PhoneNumber));

        var paginatedDatas = datas.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        int datasCount = await datas.CountAsync();


        var dataList = new List<GetAllByFilterHelpRequestQueryResponse>();
        foreach (var item in paginatedDatas)
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
                    Name = GetAllCities.Where(x => x.Id == item.RegionId).FirstOrDefault().Name,
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
