using System;
using System.ComponentModel.DataAnnotations;

namespace Psychology_API.Dtos.ReceptionDto
{
    public class ReceptionCheckFreeTimeDto
    {
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime DateTimeReception { get; set; }
    }
}