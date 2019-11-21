using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos
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
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Пароль должен содержать от 2 до 20 символов")]
        public string Password { get; set; }
    }
}