namespace E.Application.CQRS.Project.Query.Response;

public class GetAllByFilterProjectQueryResponse
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public string Header { get; set; }
    public string FileUrl { get; set; }
    public int UploadFileId { get; set; }

    public string Content { get; set; }
}

