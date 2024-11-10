using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Blog.Command.Response;
using E.Application.CQRS.Volunteer.Command.Request;
using E.Application.CQRS.Volunteer.Command.Response;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E.Application.CQRS.Volunteer.Handler.Command
{
    public class DeleteVolunteerCommandHandler : IRequestHandler<DeleteVolunteerCommandRequest, TypedResponseModel<DeleteVolunteerCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteVolunteerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TypedResponseModel<DeleteVolunteerCommandResponse>> Handle(DeleteVolunteerCommandRequest request, CancellationToken cancellationToken)
        {
            var volunteer = await _unitOfWork.VolunteerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (volunteer == null)
            {
                throw new BadRequestException("not Found");
            }

            await _unitOfWork.VolunteerRepository.DeleteAsync(volunteer, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
            return new TypedResponseModel<DeleteVolunteerCommandResponse>
            {
                Data = new DeleteVolunteerCommandResponse
                {
                    Message = "Succesfully deleted"
                }
            };

        }
    }
}
