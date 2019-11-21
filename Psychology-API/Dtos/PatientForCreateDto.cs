using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos
{
    public class PatientForCreateDto
    {
        [Required(ErrorMessage = "Нужно обязательно указать номер личного дела")]
        public int PersonalCardNumber { get; set; }
        [Required(ErrorMessage = "Нужно обязательно указать фамилию пациента")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Нужно обязательно указать имя пациента")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Нужно обязательно указать отчество пациента")]
        public string Middlename { get; set; }
        [Required(ErrorMessage = "Нужно обязательно указать идентификатор лечащего врача")]
        public int DoctorId { get; set; }
    }
}