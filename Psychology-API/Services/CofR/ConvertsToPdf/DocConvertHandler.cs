using Microsoft.Extensions.Logging;

namespace Psychology_API.Servises.COfR.ConvertsToPdf
{
    /// <summary>
    /// Класс для ковертации doc документа в pdf
    /// </summary>
    public class DocConvertHandler : AbstractConverter
    {
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public DocConvertHandler(ILogger<AbstractConverter> logger) : base(logger)
        {
        }
        public override void ConvertToPdf(byte[] document, string extension)
        {
            if(extension.Equals("doc") || extension.Equals("docx"))
            {
                //TODO: добавить нугет пакет доя конвертации 
            }
            else
            {
                base.ConvertToPdf(document, extension);
            }
        }
    }
}