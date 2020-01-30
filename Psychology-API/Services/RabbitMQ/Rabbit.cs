using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Psychology_API.Settings;
using Psychology_Domain.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Psychology_API.Services.RabbitMQ
{
    /// <summary>
    /// Сервис для работы с брокером сообщении RabbitMQ.
    /// </summary>
    public class Rabbit
    {
        private readonly RabbitMQSettings _settingsRabbit;
        private readonly IServiceProvider _serviceProvider;
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
        /// Отправка документа в очередь для проверки.
        /// </summary>
        /// <param name="document"> Документа пациента. </param>
        public void Request(Document document)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settingsRabbit.HostName,
                Port = _settingsRabbit.Port,
                UserName = _settingsRabbit.Username,
                Password = _settingsRabbit.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "Test",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = document.Body;

            channel.BasicPublish(exchange: "", routingKey: "Test", basicProperties: null, body: body);
        }
        /// <summary>
        /// Получить результат проверки конкретного документа из очереди.
        /// </summary>
        /// <param name="documentId"> Идентификатор документа. </param>
        public void Response(int documentId)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Test",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Пришло сообщение: {0}", message);
                };
                channel.BasicConsume(queue: "Test",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}