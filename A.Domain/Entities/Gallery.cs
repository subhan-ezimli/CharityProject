namespace A.Domain.Entities;

public class Gallery
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public int UploadFileId { get; set; }
}
