using System.Collections.Generic;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Тест.
    /// </summary>
    public class Test : DomainEntity
    {
        /// <summary>
        /// Наименование теста.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Описание теста.
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
        /// <summary>
        /// Инструкция поп проведению.
        /// </summary>
        /// <value></value>
        public string Instruction { get; set; }
        /// <summary>
        /// Вопросы.
        /// </summary>
        /// <value></value>
        public ICollection<Question> Questions { get; set; }
        /// <summary>
        /// Ответы.
        /// </summary>
        /// <value></value>
        public ICollection<Answer> Answers { get; set; }
        /// <summary>
        /// Интерпритация результата.
        /// </summary>
        /// <value></value>
        public ICollection<ProcessingInterpretationOfResult> ProcessingInterpretationOfResults { get; set; }

    }
}