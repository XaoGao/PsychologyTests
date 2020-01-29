using System;

namespace Psychology_API.Dtos
{
    public class ReceptionForCreateDto
    {
        public DateTime DateTimeReception { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}