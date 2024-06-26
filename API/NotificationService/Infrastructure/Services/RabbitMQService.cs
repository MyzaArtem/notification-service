using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class RabbitMQService : BackgroundService
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly IModel _channel;
        private readonly string _notificationQueue;
        private readonly string _deletionQueue;

        public RabbitMQService(ILogger<RabbitMQService> logger, IOptions<RabbitMQOptions> options)
        {
            _logger = logger;

            var factory = new ConnectionFactory()
            {
                HostName = options.Value.Host,
                UserName = options.Value.Username,
                Password = options.Value.Password
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _notificationQueue = options.Value.NotificationQueue;
            _deletionQueue = options.Value.DeletionQueue;

            _channel.QueueDeclare(queue: _notificationQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: _deletionQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (ea.RoutingKey == _notificationQueue)
                {
                    await HandleNewNotification(message);
                }
                else if (ea.RoutingKey == _deletionQueue)
                {
                    await HandleNotificationDeletion(message);
                }
            };

            _channel.BasicConsume(queue: _notificationQueue, autoAck: true, consumer: consumer);
            _channel.BasicConsume(queue: _deletionQueue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private Task HandleNewNotification(string message)
        {
            _logger.LogInformation($"Received new notification: {message}");

            var notification = JsonSerializer.Deserialize<Notification>(message);

            //TODO: Сохранить уведомление в базу данных

            return Task.CompletedTask;
        }

        private Task HandleNotificationDeletion(string message)
        {
            _logger.LogInformation($"Received deletion notification: {message}");

            var deletionInfo = JsonSerializer.Deserialize<NotificationDeletion>(message);

            //TODO: Удалить уведомление из базы данных (добавьте вашу логику)

            return Task.CompletedTask;
        }
    }

    public class RabbitMQOptions
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NotificationQueue { get; set; }
        public string DeletionQueue { get; set; }
    }

    public class NotificationDeletion
    {
        public string ServiceName { get; set; }
        public string NotificationId { get; set; }
    }
}
