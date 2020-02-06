using System;
using System.Collections.Generic;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Результат тестирования пациента.
    /// </summary>
    public class PatientTestResult : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор доктора.
        /// </summary>
        /// <value></value>
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Идентификатор пациента.
        /// </summary>
        /// <value></value>
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        /// <summary>
        /// Идентифкатор теста.
        /// </summary>
        /// <value></value>
        public int TestId { get; set; }
        public Test Test { get; set; }
        /// <summary>
        /// Количество баллов по тесту.
        /// </summary>
        /// <value></value>
        public int TestResultInPoints { get; set; }
        /// <summary>
        /// Идентифкатор интерпритации теста.
        /// </summary>
        /// <value></value>
        public int ProcessingInterpretationOfResultId { get; set; }
        public ProcessingInterpretationOfResult ProcessingInterpretationOfResult { get; set; }
        /// <summary>
        /// Дата и время прохождения теста.
        /// </summary>
        /// <value></value>
        public DateTime DateTimeCreate { get; set; }
        /// <summary>
        /// Ссылка на коллекцию Вопрос-ответ.
        /// </summary>
        /// <value></value>
        public ICollection<QuestionAnswer> QuestionsAnswers { get; set; }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        public PatientTestResult()
        {
            
        }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="testResultInPoints"> Количество баллов по тесту. </param>
        /// <param name="processingInterpretationOfResultId">Идентификатор интерпритации теста. </param>
        /// <param name="dateTimeCreate"> Дата и время прохождения теста. </param>
        /// <param name="questionAnswers"> Коллекция Вопрос-ответ. </param>
        public PatientTestResult(int doctorId, int patientId, int testId, int testResultInPoints, int processingInterpretationOfResultId, DateTime dateTimeCreate, ICollection<QuestionAnswer> questionAnswers)
        {
            if(doctorId <= 0)
                throw new ArgumentNullException(nameof(doctorId), "Идентификатор не может быть 0 или меньше");
            if(patientId <= 0)
                throw new ArgumentNullException(nameof(patientId), "Идентификатор не может быть 0 или меньше");
            if(questionAnswers.Count <= 0)
                throw new ArgumentNullException(nameof(questionAnswers), "Идентификатор не может быть 0 или меньше");
            if(processingInterpretationOfResultId <= 0)
                throw new ArgumentNullException(nameof(processingInterpretationOfResultId), "Идентификатор не может быть 0 или меньше");
            if(processingInterpretationOfResultId < 0)
                throw new ArgumentNullException(nameof(processingInterpretationOfResultId), "Количество очков на тест не может быть меньше 0");
            if(testId <= 0)
                throw new ArgumentNullException(nameof(testId), "Идентификатор не может быть 0 или меньше");
            
            DoctorId = doctorId;
            PatientId = patientId;
            ProcessingInterpretationOfResultId = processingInterpretationOfResultId;
            DateTimeCreate = dateTimeCreate;
            QuestionsAnswers = questionAnswers;
            TestResultInPoints = testResultInPoints;
            TestId = testId;
        }
    }
}