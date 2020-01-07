namespace Psychology_API.Servises.ConvertsToPdf
{
    /// <summary>
    /// Цепочка ответственных для конвертации разных докумнетов в pdf формат.
    /// </summary>
    public interface IConvertHandler
    {
        /// <summary>
        /// Указать следующего ответственного для конвертации.
        /// </summary>
        /// <param name="handler"> Следующий отвественный. </param>
        /// <returns></returns>
        IConvertHandler SetNext(IConvertHandler handler);
        /// <summary>
        /// Конвертация документа в pdf формат.
        /// </summary>
        /// <param name="document"> Входящий документов в виде массива байтов. </param>
        /// <param name="extension"> Расширение входящего документа. </param>
        void ConvertToPdf(byte[] document, string extension);
    }
}