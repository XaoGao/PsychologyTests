using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.DoctorDto
{
    /// <summary>
    /// Класс для аутентификации в системе.
    /// </summary>
    public class DoctorForLoginDto
    {
        [Required(ErrorMessage = "Обязательно укажите логин.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Обязательно укажите пароль.")]
        public string Password { get; set; }
    }
}