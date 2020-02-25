using System;
using System.Collections.Generic;
using Psychology_API.Dtos.DoctorDto;
using Psychology_API.Dtos.PatientDto;
using Psychology_API.Dtos.TestDto;
using Psychology_Domain.Domain;

namespace Psychology_API.Dtos.PatientTestResultDto
{
    public class PatientTestResultForReturnDetailDto
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
        public ICollection<QuestionAnswer> QuestionsAnswers { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}