using System;

namespace Psychology_API.Dtos
{
    public class ReceptionCheckFreeTimeDto
    {
        public int DoctorId { get; set; }
        public DateTime DateTimeReception { get; set; }
    }
}