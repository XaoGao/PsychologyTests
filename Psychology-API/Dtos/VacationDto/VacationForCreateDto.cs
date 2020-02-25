using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.VacationDto
{
    /// <summary>
    /// Класс по созданию отпуска.
    /// </summary>
    public class VacationForCreateDto
    {
        [Required(ErrorMessage = "Обязательно укажите идентификатор доктора.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Обязательно укажите начало периода отпуска.")]
        public DateTime StartVacation { get; set; }
        [Required(ErrorMessage = "Обязательно укажите конец периода отпуска.")]
        public DateTime EndVacation { get; set; }
    }
}