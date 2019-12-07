using System;

namespace Psychology_Domain.Domain
{
    public class Vacation
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime StartVacation { get; set; }
        public DateTime EndVacation { get; set; }
        public int CountDays { get; set; }
    }
}