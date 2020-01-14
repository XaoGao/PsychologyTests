namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Ответ.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
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