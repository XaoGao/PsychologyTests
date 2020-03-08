using Microsoft.Extensions.Configuration;

namespace Psychology_API.Settings
{
    /// <summary>
    /// Настройка брокера сообщения.
    /// </summary>
    public class RabbitMQSettings
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Создать экземпляр класса.
        /// </summary>
        /// <param name="configuration"></param>
        public RabbitMQSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// IP адрес до брокера сообщения.
        /// </summary>
        /// <typeparam name="string"> Расположение брокера. </typeparam>
        /// <returns></returns>
        public string HostName => _configuration.GetValue<string>("Rabbitmq:HostName");
        /// <summary>
        /// Порт на котором крутится брокер.
        /// </summary>
        /// <typeparam name="int"> Порт брокера. </typeparam>
        /// <returns></returns>
        public int Port        => _configuration.GetValue<int>("Rabbitmq:Port");
        /// <summary>
        /// Логин для обращению к брокеру.
        /// </summary>
        /// <typeparam name="string"> Логин. </typeparam>
        /// <returns></returns>
        public string Username => _configuration.GetValue<string>("Rabbitmq:Username");
        /// <summary>
        /// Пароль для доступа к брокеру.
        /// </summary>
        /// <typeparam name="string"> Пароль. </typeparam>
        /// <returns></returns>
        public string Password => _configuration.GetValue<string>("Rabbitmq:Password");
        /// <summary>
        /// Uri подклчения к брокеру.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public string Uri => _configuration.GetValue<string>("Rabbitmq:Uri");
    }
}