using System;
using Microsoft.Extensions.Logging;

namespace Psychology_API.Servises.COfR.ConvertsToPdf
{
    /// <summary>
    /// Базовый класс от которого должны наследоваться все остальные обработчики.
    /// </summary>
    public class AbstractConverter : IConvertHandler
    {
        private readonly ILogger<AbstractConverter> _logger;
        /// <summary>
        /// Создать новый экземпляр класса.
        /// </summary>
        /// <param name="logger"> Логирование. </param>
        public AbstractConverter(ILogger<AbstractConverter> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Ссылка на следующий обработчик.
        /// </summary>
        private IConvertHandler next;
        /// <summary>
        /// Установить следующий обработчик в цепочке.
        /// </summary>
        /// <param name="handler"> Обработчик. </param>
        /// <returns></returns>
        public IConvertHandler SetNext(IConvertHandler handler)
        {
            next = handler;

            return handler;
        }

        /// <summary>
        /// Базовый обработчик, если не поддерживается формат документа, то записывает в лог и выдает Exception.
        /// </summary>
        /// <param name="document"> Входящий документа в виде массива байтов. </param>
        /// <param name="extension"> Расширение документа. </param>
        public virtual void ConvertToPdf(byte[] document, string extension)
        {
            if (next != null)
            {
                next.ConvertToPdf(document, extension);
            }
            else
            {
                _logger.LogError($"{DateTime.Now} пытались сконвертировать документ{extension} формата, не существует нужного обработчика.");
                throw new Exception($"{extension} формат не обрабатывается.");
            }
        }
    }
}