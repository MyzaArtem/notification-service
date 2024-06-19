using Microsoft.Extensions.Hosting;
using NotificationService.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.Integration
{
    public interface IMessageBusClient
    {
        void PublishNewNotification(NotificationPublishedDto notificationPublishedDto);
    }
}
