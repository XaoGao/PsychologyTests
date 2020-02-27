using System.Collections.Generic;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Воспрос из теста.
    /// </summary>
    public class Question : DomainEntity
    {
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