using System;

namespace Psychology_Domain.Domain
{
    public class PatientTestResult
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int TestResultInPoints { get; set; }
        public int ProcessingInterpretationOfResultId { get; set; }
        public ProcessingInterpretationOfResult ProcessingInterpretationOfResult { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}