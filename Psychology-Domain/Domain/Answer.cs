using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Ответ на вопрос из теста.
    /// </summary>
    public class Answer : DomainEntity
    {
        /// <summary>
        /// Текст ответа.
        /// </summary>
        /// <value></value>
        public string Text { get; set; }
        /// <summary>
        /// Значение.
        /// </summary>
        /// <value></value>
        public int Value { get; set; }
        /// <summary>
        /// Идентификатор вопроса.
        /// </summary>
        /// <value></value>
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}