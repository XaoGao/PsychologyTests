using Microsoft.Extensions.Logging;

namespace Psychology_API.Services.COfR.ConvertsToPdf
{
    /// <summary>
    /// Создает цепочку отвественных за коневертацию документов в формат pdf.
    /// </summary>
    public class ManagerConvertHandler
    {
        IConvertHandler convertHandler;
        private readonly ILogger<AbstractConverter> _logger;
        /// <summary>
        /// Создать новый экземпляр класса.
        /// </summary>
        /// <param name="logger"> Логирование. </param>
        public ManagerConvertHandler(ILogger<AbstractConverter> logger)
        {
            _logger = logger;
            convertHandler = new DocConvertHandler(logger);
        }
        /// <summary>
        /// Добавить в цепочку конвертер XML to pdf
        /// </summary>
        public void SetXMLConvertHandler()
        {

        }
        /// <summary>
        /// Добавить в цепочку конвертер jpeg to pdf
        /// </summary>
        public void SetJPEGConvertHandler()
        {

        }
    }
}