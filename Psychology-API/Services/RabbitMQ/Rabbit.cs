using System;
using Microsoft.Extensions.DependencyInjection;
using Psychology_API.Settings;
using RabbitMQ.Client;

namespace Psychology_API.Services.RabbitMQ
{
    /// <summary>
    /// Сервис для работы с брокером сообщении RabbitMQ.
    /// </summary>
    public class Rabbit : IBroker
    {
        private readonly RabbitMQSettings _settingsRabbit;
        private readonly IServiceProvider _serviceProvider;
        private const string ROUTING_KEY_REQUEST = "Document-Request";
        /// <summary>
        /// Создание экземпляра класса.
        /// </summary>
        /// <param name="serviceProvider">  </param>
        public Rabbit(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _settingsRabbit = _serviceProvider.GetRequiredService<RabbitMQSettings>();
        }
        /// <summary>
        /// Отправка объекта в очередь брокера.
        /// </summary>
        /// <param name="entity"> Объект для отправки в очереь. </param>
        public bool Request(byte[] entity)
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.Uri = new System.Uri(_settingsRabbit.Uri);

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: ROUTING_KEY_REQUEST,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var body = entity;

                channel.BasicPublish(exchange: "", routingKey: ROUTING_KEY_REQUEST, basicProperties: null, body: body);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}