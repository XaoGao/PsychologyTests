using System;
using Microsoft.Extensions.DependencyInjection;
using Psychology_API.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Psychology_API.Services.RabbitMQ
{
    /// <summary>
    /// Сервис для работы с брокером сообщении RabbitMQ.
    /// </summary>
    public class Rabbit : IBroker
    {
        private readonly RabbitMQSettings _settingsRabbit;
        private readonly IServiceProvider _serviceProvider;
        private const string ROUTING_KEY_RESPONSE = "Document-Response";
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
        public void Request(byte[] entity)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri(_settingsRabbit.Uri);
            // {
            //     HostName = _settingsRabbit.HostName,
            //     // Port = _settingsRabbit.Port,
            //     UserName = _settingsRabbit.Username,
            //     Password = _settingsRabbit.Password
            // };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: ROUTING_KEY_REQUEST,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: true,
                                 arguments: null);

            var body = entity;

            channel.BasicPublish(exchange: "", routingKey: ROUTING_KEY_REQUEST, basicProperties: null, body: body);
        }
        /// <summary>
        /// Получить объект из очереди.
        /// </summary>
        public byte[] Response()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri(_settingsRabbit.Uri); 
            // {
            //     HostName = _settingsRabbit.HostName,
            //     // Port = _settingsRabbit.Port,
            //     UserName = _settingsRabbit.Username,
            //     Password = _settingsRabbit.Password
            // };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: ROUTING_KEY_RESPONSE,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: true,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                byte[] result = null;
                consumer.Received += (model, ea) =>
                {
                    result = ea.Body;
                };
                channel.BasicConsume(queue: ROUTING_KEY_RESPONSE,
                                     autoAck: true,
                                     consumer: consumer);
                return result;
            }
        }
    }
}