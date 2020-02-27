using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Таблица для логирования ошибок.
    /// </summary>
    public class Log : DomainEntity
    {
        /// <summary>
        /// Уровень лога.
        /// </summary>
        /// <value></value>
        public string LevelLog { get; set; }
        /// <summary>
        /// Поле для записи тела.
        /// </summary>
        /// <value></value>
        public string Body { get; set; }
        /// <summary>
        /// Текст ошибки.
        /// </summary>
        /// <value></value>
        public string Text { get; set; }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="text"> Текст ошибки. </param>
        public Log(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text), "Текст для записи в лог не может быть пустым");

            Text = text;
            Create = DateTime.Now;
        }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        public Log()
        {
            Create = DateTime.Now;
        }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="levelLog"> Уровень лога. </param>
        /// <param name="body"> Сообщение. </param>
        public Log(string levelLog, string body)
        {
            if(string.IsNullOrWhiteSpace(levelLog))
                throw new ArgumentNullException(nameof(levelLog), "Уровень лога не может быть пустым");

            if(string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException(nameof(body), "Сообщение для лога не может быть пустым");

            LevelLog = levelLog;
            Body = body;
            Create = DateTime.Now;
        }
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="levelLog"> Уровень лога. </param>
        /// <param name="body"> Сообщение. </param>
        /// <param name="text"> Текст ошибки. </param>
        public Log(string levelLog, string body, string text)
        {
            if(string.IsNullOrWhiteSpace(levelLog))
                throw new ArgumentNullException(nameof(levelLog), "Уровень лога не может быть пустым");

            if(string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException(nameof(body), "Сообщение для лога не может быть пустым");

            if(string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text), "Текст для записи в лог не может быть пустым");

            LevelLog = levelLog;
            Body = body;
            Text = text;
            Create = DateTime.Now;
        }
    }
}