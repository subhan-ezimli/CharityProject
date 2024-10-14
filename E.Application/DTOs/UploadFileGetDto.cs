namespace E.Application.DTOs
{
    public class UploadFileGetDto
    {
        public int id { get; set; }
        public string mimeType { get; set; }
        public DateTime uploadDate { get; set; }
        public long size { get; set; }
        public string name { get; set; }
        public string fileNameOnDisk { get; set; }
        public string fileUrl
        {
            get
            {
                return $"https://dev.optima.az:8317/api/file/openFile/{fileNameOnDisk}";
            }
        }
    }

}
