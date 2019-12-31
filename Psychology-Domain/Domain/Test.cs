using System.Collections.Generic;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Тест.
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
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
        public ICollection<ProcessingInterpretationOfResult> ProcessingInterpretationOfResults { get; set; }

    }
}