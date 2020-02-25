using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.DoctorDto
{
    public class DoctorForUpdateDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Middlename { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public int PhoneId { get; set; }
    }
}