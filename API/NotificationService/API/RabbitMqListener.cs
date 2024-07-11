using API.Hubs;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Application.DTOs;
using System.Text;
using Newtonsoft.Json;

namespace API
{
    /*public class RabbitMqListener : IRabbitMqListener
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;

        protected readonly IServiceProvider _serviceProvider;

        public RabbitMqListener(IServiceProvider serviceProvider)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _serviceProvider = serviceProvider;
        }

        public virtual void Connect()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += delegate (object model, BasicDeliverEventArgs ea)
            {
                var chatHub = (IHubContext<NotificationHub>)_serviceProvider.GetService(typeof(IHubContext<NotificationHub>));

                chatHub.Clients.All.SendAsync("ReceiveNewNotifications", "You have received a message");

            };

            _channel.BasicConsume(queue: "CreateQueue", autoAck: true, consumer: consumer);

            Console.WriteLine("Connect subscribe on CreateQueue");
        }
    }*/
}
