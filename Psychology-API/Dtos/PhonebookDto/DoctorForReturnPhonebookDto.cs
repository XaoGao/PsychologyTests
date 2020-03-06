using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.PhonebookDto
{
    public class DoctorForReturnPhonebookDto
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Fullname { get => $"{Lastname} {Firstname} {Middlename}";}
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        
    }
}