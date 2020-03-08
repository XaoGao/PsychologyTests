namespace Psychology_API.Services.RabbitMQ
{
    /// <summary>
    /// Интерфейс для брокера сообщении.
    /// </summary>
    public interface IBroker
    {
        /// <summary>
        /// Отправить данные в очередь.
        /// </summary>
        /// <param name="entity"> Объект в виде массива байтов. </param>
        /// <returns> True если объект был успешно отправен. </returns>
        bool Request(byte[] entity);
    }
}