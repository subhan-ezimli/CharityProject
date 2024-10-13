namespace A.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public int UploadFileId { get; set; }
        public string Content { get; set; }
    }
}
