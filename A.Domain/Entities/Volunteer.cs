namespace A.Domain.Entities
{
    public class Volunteer
    {
        public int Id { get; set; }
        public bool Isdeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int UploadFileId { get; set; }
    }   
}
