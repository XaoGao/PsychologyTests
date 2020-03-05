using Microsoft.Extensions.Logging;

namespace Psychology_API.Services.COfR.ConvertsToPdf
{
    /// <summary>
    /// Класс для ковертации jpeg документа в pdf
    /// </summary>
    public class JpegConvertHandler : AbstractConverter
    {
        /// <summary>
        /// Создать новый экземпляр класса.
        /// </summary>
        /// <param name="logger"> Логгирование. </param>
        /// <returns></returns>
        public JpegConvertHandler(ILogger<AbstractConverter> logger) : base(logger)
        {
        }
        public override void ConvertToPdf(byte[] document, string extension)
        {
            if(extension.Equals("jpeg"))
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