using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.DoctorDto
{
    /// <summary>
    /// Класс по обновлению данных пользователя.
    /// </summary>
    public class DoctorForUpdateDto
    {
        [Required(ErrorMessage = "Обязательно укажите логин.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Обязательно укажите фамилию.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Обязательно укажите имя.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Обязательно укажите отчество.")]
        public string Middlename { get; set; }
        [Required(ErrorMessage = "Обязательно укажите дату рождения.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор отдела.")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор должности.")]
        public int PositionId { get; set; }
        public int PhoneId { get; set; }
        public bool IsLock { get; set; }
    }
}