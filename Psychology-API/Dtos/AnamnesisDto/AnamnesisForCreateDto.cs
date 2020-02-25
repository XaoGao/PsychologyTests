using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.AnamnesisDto
{
    /// <summary>
    /// Класс создания анамнесиса.
    /// </summary>
    public class AnamnesisForCreateDto
    {
        [Required(ErrorMessage = "Обязательно укажите идентификатор пациента.")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Обязательно укажите идентификатор врача.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Обязательно укажите текст анамнезиса.")]
        public string Conclusion { get; set; }
    }
}