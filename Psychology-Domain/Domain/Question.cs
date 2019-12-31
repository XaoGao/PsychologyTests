using System.Collections.Generic;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Воспрос.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Текст вопроса.
        /// </summary>
        /// <value></value>
        public string Text { get; set; }
        /// <summary>
        /// Порядковый номер.
        /// </summary>
        /// <value></value>
        public int sortLevel { get; set; }
        /// <summary>
        /// Идентификатор теста к которому привязаны вопросы.
        /// </summary>
        /// <value></value>
        public int TestId { get; set; }
        public Test Test { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}