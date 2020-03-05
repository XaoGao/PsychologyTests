using Microsoft.Extensions.Logging;

namespace Psychology_API.Services.COfR.ConvertsToPdf
{
    /// <summary>
    /// /// Класс для ковертации xml документа в pdf
    /// </summary>
    public class XMLConvertHandler : AbstractConverter
    {
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public XMLConvertHandler(ILogger<AbstractConverter> logger) : base(logger)
        {
        }
        public override void ConvertToPdf(byte[] document, string extension)
        {
            if(extension.Equals("xml"))
            {
                //TODO: добавить нугет пакет для конвертации 
            }
            else
            {
                base.ConvertToPdf(document, extension);
            }
        }
    }
}