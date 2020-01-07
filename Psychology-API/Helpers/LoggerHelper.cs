using Microsoft.Extensions.Logging;
using Psychology_API.Data;
using Psychology_Domain.Domain;

namespace Psychology_API.Helpers
{
    /// <summary>
    /// Расширенное логирование.
    /// </summary>
    /// <typeparam name="T"> Класс, которой отслеживает лог. </typeparam>
    /// TODO: выполнить миграцию для данного класса.
    public class LoggerHelper<T> where T : class
    {
        // private readonly DataContext _context;
        // private readonly ILogger<T> _logger;
        // /// <summary>
        // /// Создание нового экземпляра класса.
        // /// </summary>
        // /// <param name="context"> Конект БД. </param>
        // /// <param name="logger"> Класс для логирования ошибок </param>
        // public LoggerHelper(DataContext context, ILogger<T> logger)
        // {
        //     _logger = logger;
        //     _context = context;
        // }
        // /// <summary>
        // /// Запись ошибки в БД.
        // /// </summary>
        // /// <param name="msg"> Текст для вывода ошибки в консоль. </param>
        // /// <param name="extensions"> Полный текст ошибки для записи в БД.</param>
        // public void SaveLog(string msg, string extensions)
        // {
        //     Log log = new Log(msg);

        //     _logger.LogError(extensions);

        //     _context.Logs.Add(log);
        //     _context.SaveChanges();
        // }
    }
}