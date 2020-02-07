using System;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos
{
    public class PatientTestResultForReturnListDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DoctorForListReturnDto Doctor { get; set; }
        public int PatientId { get; set; }
        public PatientForListDto Patient { get; set; }
        public int TestId { get; set; }
        public TestForReturnListDto Test { get; set; }
        public int TestResultInPoints { get; set; }
        public int ProcessingInterpretationOfResultId { get; set; }
        public ProcessingInterpretationOfResult ProcessingInterpretationOfResult { get; set; }
        public DateTime DateTimeCreate { get; set; }

    }
}