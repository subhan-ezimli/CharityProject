using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Volunteer.Command.Request;
using E.Application.CQRS.Volunteer.Command.Response;
using MediatR;

namespace E.Application.CQRS.Volunteer.Handler.Command;

public class CreateVolunteerCommandHandler : IRequestHandler<CreateVolunteerCommandRequest, TypedResponseModel<CreateVolunteerCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateVolunteerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TypedResponseModel<CreateVolunteerCommandResponse>> Handle(CreateVolunteerCommandRequest request, CancellationToken cancellationToken)
    {

        //check email yaz bura uchun
        var volunteer = new A.Domain.Entities.Volunteer()
        {
            Address = request.Address,
            BirthDate = request.BirthDate,
            Email = request.Email,
            FathersName = request.FathersName,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Surname = request.Surname,
            UploadFileId = request.UploadFileId
        };

        await _unitOfWork.VolunteerRepository.AddAsync(volunteer, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new TypedResponseModel<CreateVolunteerCommandResponse>()
        {
            Data = new CreateVolunteerCommandResponse()
            {
                Message = "Successfully created"
            }
        };
    }
}
