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
        /// <typeparam name="T"> Данные, которые отправляются в очередь. </typeparam>
        bool Request(byte[] entity);
    }
}