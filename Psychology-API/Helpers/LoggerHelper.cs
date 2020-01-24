using System;
using Microsoft.Extensions.Logging;
using Psychology_API.Data;
using Psychology_Domain.Domain;
using System.IO;

namespace Psychology_API.Helpers
{
    /// <summary>
    /// Расширенное логирование.
    /// </summary>
    /// <typeparam name="T"> Класс, которой отслеживает лог. </typeparam>
    public class LoggerHelper<T> where T : class
    {
        private readonly DataContext _context;
        private readonly ILogger<T> _logger;
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="context"> Конект БД. </param>
        /// <param name="logger"> Класс для логирования ошибок </param>
        public LoggerHelper(DataContext context, ILogger<T> logger)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Запись ошибки в БД.
        /// </summary>
        /// <param name="msg"> Текст ошибки. </param>
        public void SaveLog(string msg)
        {
            ConsoleLogger(msg);
            FileLogger(msg);
            DBLogger(msg);
        }
        private void ConsoleLogger(string msg)
        {
            // Console.WriteLine($"{DateTime.Now} : Ошибка: {msg}");
            _logger.LogError($"{DateTime.Now} : Ошибка: {msg}");
        }
        private void FileLogger(string msg)
        {
            string path = Directory.GetCurrentDirectory();
            using var sw = new StreamWriter(path, true);
            sw.WriteLine($"{DateTime.Now} : Ошибка: {msg}");
        }
        private void DBLogger(string msg)
        {
            Log log = new Log(msg);

            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}