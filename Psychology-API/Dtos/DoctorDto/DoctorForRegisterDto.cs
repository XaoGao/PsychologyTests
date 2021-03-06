using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.DoctorDto
{
    public class DoctorForRegisterDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Middlename { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 20 символов")]
        public string Password { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int PositionId { get; set; }
        public int PhoneId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}