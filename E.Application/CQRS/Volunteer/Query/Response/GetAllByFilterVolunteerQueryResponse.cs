namespace E.Application.CQRS.Volunteer.Query.Response;

public class GetAllByFilterVolunteerQueryResponse
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }
    public DateTime BirthDate { get; set; }

    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public string FileUrl { get; set; }
    public int UploadFileId { get; set; }
}
