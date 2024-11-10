using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Volunteer.Command.Request;
using E.Application.CQRS.Volunteer.Command.Response;
using MediatR;

namespace E.Application.CQRS.Volunteer.Handler.Command
{
    public class UpdateVolunteerCommandHandler : IRequestHandler<UpdateVolunteerCommandRequest, TypedResponseModel<UpdateVolunteerCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateVolunteerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TypedResponseModel<UpdateVolunteerCommandResponse>> Handle(UpdateVolunteerCommandRequest request, CancellationToken cancellationToken)
        {
            var volunteer = await _unitOfWork.VolunteerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (volunteer == null)
            {
                throw new BadRequestException("not found");
            }

            volunteer.BirthDate = request.BirthDate;
            volunteer.Surname = request.Surname;
            volunteer.PhoneNumber = request.PhoneNumber;
            volunteer.Address = request.Address;
            volunteer.Email = request.Email;
            volunteer.UploadFileId = request.UploadFileId;
            volunteer.FathersName = request.FathersName;
            volunteer.Name = request.Name;

            await _unitOfWork.VolunteerRepository.UpdateAsync(volunteer, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            return new TypedResponseModel<UpdateVolunteerCommandResponse>()
            {
                Data = new UpdateVolunteerCommandResponse()
                {
                    Message = "Successfully updated "
                }
            };
        }
    }
}
