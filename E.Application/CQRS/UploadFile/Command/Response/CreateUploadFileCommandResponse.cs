namespace E.Application.CQRS.UploadFile.Command.Response;

public class CreateUploadFileCommandResponse
{
    public int Id { get; set; }
    public string MimeType { get; set; }
    public DateTime CreatedDate { get; set; }
    public long FileSize { get; set; }
    public string Name { get; set; }
    public string FileUrl
    {
        get
        {
            return $"http://localhost:5245/api/UploadFile/download/{Id}";
        }
    }
}