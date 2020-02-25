using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.VacationDto
{
    public class VacationForCreateDto
    {
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime StartVacation { get; set; }
        [Required]
        public DateTime EndVacation { get; set; }
    }
}