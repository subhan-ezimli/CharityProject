using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.HelpRequest.Command.Response;
using MediatR;

namespace E.Application.CQRS.HelpRequest.Command.Request;

public class UpdateHelpRequestCommandRequest : IRequest<TypedResponseModel<UpdateHelpRequestCommandResponse>>
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }
    public string PhoneNumber { get; set; }
    public int RegionId { get; set; }
    public string Address { get; set; }
    public string ShortInfo { get; set; }

}
