using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using System.Text;

namespace NotificationService.AsyncDataService
{
    public class RabbitMqSubscriber : BackgroundService, IMessageBusSubscriber
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        Task IMessageBusSubscriber.ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
