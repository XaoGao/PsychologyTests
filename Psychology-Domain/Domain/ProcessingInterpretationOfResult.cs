using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Интерпритация результатов.
    /// </summary>
    public class ProcessingInterpretationOfResult : DomainEntity
    {
        /// <summary>
        /// Минимальное количество очков по ответам.
        /// </summary>
        /// <value></value>
        public int MinValue { get; set; }
        /// <summary>
        /// Максимальное количество очков по ответам.
        /// </summary>
        /// <value></value>
        public int MaxValue { get; set; }
        /// <summary>
        /// Описание результата.
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
        /// <summary>
        /// Идентификатор теста.
        /// </summary>
        /// <value></value>
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}