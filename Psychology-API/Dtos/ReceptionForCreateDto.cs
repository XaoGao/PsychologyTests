using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos
{
    public class ReceptionForCreateDto
    {
        [Required]
        public DateTime DateTimeReception { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int PatientId { get; set; }
    }
}