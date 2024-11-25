using B.Repository.Common;
using C.Common.Extensions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.User.Query.Request;
using E.Application.CQRS.User.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.CQRS.User.Handler.QueryHandler
{
    public class UserGetAllByFilterQueryHandler : IRequestHandler<UserGetAllByFilterQueryRequest, ResponseModelPagination<UserGetAllByFilterQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserGetAllByFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModelPagination<UserGetAllByFilterQueryResponse>> Handle(UserGetAllByFilterQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = _unitOfWork.UserRepository.GetAllAsQueryable();
            datas = datas.WhereIf(request.Name, x => x.Name.ToLower().Contains(request.Name.ToLower()))
                        .WhereIf(request.Surname, x => x.Surname.ToLower().Contains(x.Surname.ToLower()))
                        .WhereIf(request.FathersName, x => x.FathersName.ToLower().Contains(x.FathersName.ToLower()))
                        .WhereIf(request.Email, x => x.Email.ToLower().Contains(request.Email.ToLower()));

            var paginatedDatas = datas.Skip(request.Limit * (request.Page - 1)).Take(request.Limit);

            var responseList = new List<UserGetAllByFilterQueryResponse>();

            foreach (var data in paginatedDatas)
            {
                var dataa = new UserGetAllByFilterQueryResponse()
                {
                    BirthDate = data.BirthDate,
                    CreatedDate = data.CreatedDate,
                    Email = data.Email,
                    FathersName = data.FathersName,
                    Id = data.Id,
                    Name = data.Name,
                    Surname = data.Surname,
                    UserRole = (int)data.UserRole,
                };
                responseList.Add(dataa);
            }
            var pagination = new Pagination<UserGetAllByFilterQueryResponse>()
            {
                Datas = responseList,
                TotalDataCount = await datas.CountAsync()
            };


            return new ResponseModelPagination<UserGetAllByFilterQueryResponse> { Data = pagination };
        }
    }
}
