using E.Application.DTOs;

namespace E.Application.CQRS.HelpRequest.Query.Response;

public class GetAllByFilterHelpRequestQueryResponse
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }
    public string ShortInfo { get; set; }

    public RegionDto region { get; set; }

}
