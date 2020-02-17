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
        void Request(byte[] entity);
        /// <summary>
        /// Получить ответ из очереди.
        /// </summary>
        /// <param name="id"> Идентификатор очереди. </param>
        byte[] Response();
    }
}