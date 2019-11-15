using System;

namespace Psychology_Domain.Domain
{
    public class PatientHistory
    {
        public int Id { get; set; }
        public DateTime DateInsert { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string ReceptionResult { get; set; }
    }
}