using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos
{
    /// <summary>
    /// Класс по созданию приема пациента.
    /// </summary>
    public class ReceptionForCreateDto
    {
        [Required(ErrorMessage = "Обязательно укажите время приема.")]
        public DateTime DateTimeReception { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор доктора.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор доктора.")]
        public int PatientId { get; set; }
    }
}