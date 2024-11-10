namespace E.Application.CQRS.Gallery.Query.Response;

public class GetALlGalleriesQueryResponse
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public string FileUrl { get; set; }
}

