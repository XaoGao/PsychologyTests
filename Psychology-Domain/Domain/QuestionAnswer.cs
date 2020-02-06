using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Ответы пациента на вопросы теста.
    /// </summary>
    public class QuestionAnswer : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пациента.
        /// </summary>
        /// <value></value>
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        /// <summary>
        /// Идентификатор теста.
        /// </summary>
        /// <value></value>
        public int TestId { get; set; }
        public Test Test { get; set; }
        /// <summary>
        /// Идентфикатор вопроса.
        /// </summary>
        /// <value></value>
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        /// <summary>
        /// Ответ на вопрос.
        /// </summary>
        /// <value></value>
        public int AnswersValue { get; set; }
    }
}