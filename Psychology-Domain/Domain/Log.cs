using System;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Таблица для логирования ошибок.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="text"> Текст ошибки. </param>
        public Log(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text), "Текст для записи в лог не может быть пустым");

            Text = text;
            DateInsert = DateTime.Now;
        }
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Текст ошибки.
        /// </summary>
        /// <value></value>
        public string Text { get; set; }
        /// <summary>
        /// Дата записи.
        /// </summary>
        /// <value></value>
        public DateTime DateInsert { get; set; }
    }
}