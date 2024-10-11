namespace A.Domain.Entities
{
    public class UploadFile
    {
        public int Id { get; set; }
        public string FileNameOnDisk { get; set; }
        public string OriginalFileName { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedDate { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }

    }
}
