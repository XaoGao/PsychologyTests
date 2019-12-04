using System;

namespace Psychology_API.Dtos
{
    public class DoctorForUpdateDto
    {
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public int PhoneId { get; set; }
    }
}