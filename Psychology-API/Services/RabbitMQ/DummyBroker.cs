namespace Psychology_API.Services.RabbitMQ
{
    /// <summary>
    /// Заглушка для брокера сообщении.
    /// </summary>
    public class DummyBroker// : IBroker
    {
        public bool Request<T>(T entity) where T : class
        {
            return true;
        }

        public bool Response(int id)
        {
            return true;
        }
    }
}