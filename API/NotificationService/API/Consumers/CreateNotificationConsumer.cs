using Application.Commands.NotificationsCommands;
using MassTransit;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using System;
using Newtonsoft.Json;
using Application.DTOs;

namespace API.Hubs;

public class CreateNotificationConsumer : IConsumer<Notification>
{
    private readonly ILogger<CreateNotificationConsumer> _logger;
    private readonly IMediator _mediator;

    protected readonly IServiceProvider _serviceProvider;

    public CreateNotificationConsumer(ILogger<CreateNotificationConsumer> logger, IMediator mediator, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
    }

    public async Task Consume(ConsumeContext<Notification> context)
    {
        /* Example?
         {
             "UserId": "00000001-0000-0000-0000-000000000002",
             "ServiceId": "00000001-0000-0000-0000-000000000002",
             "NotificationTypeId": "00000001-0000-0000-0000-000000000002",
             "NotificationCategoryId": "00000001-0000-0000-0000-000000000002",
             "Title": "notification-title",
             "Message": "notification-message",
             "CreatedAt": "2022-01-01T12:00:00Z",
             "ReadAt": "2022-01-01T12:00:00Z"
           }
         */
        try
        {
            _logger.LogInformation("Получение уведомления из очереди");
            
            var notificationReadDto = await _mediator.Send(new CreateNotificationCommand(context.Message));

            var chatHub = (IHubContext<NotificationHub>)_serviceProvider.GetService(typeof(IHubContext<NotificationHub>));

            Notification a = new Notification();

            a.UserId = context.Message.UserId;
            a.ServiceId = context.Message.ServiceId;
            a.NotificationTypeId = context.Message.NotificationTypeId;
            a.NotificationCategoryId = context.Message.NotificationCategoryId;
            a.Title = context.Message.Title;
            a.Message = context.Message.Message;
            a.CreatedAt = context.Message.CreatedAt;
            a.Status = context.Message.Status;

            var temp = JsonConvert.SerializeObject(a);

            chatHub.Clients.All.SendAsync("ReceiveNewNotifications", temp);

            await context.NotifyConsumed(TimeSpan.FromSeconds(1), nameof(CreateNotificationConsumer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при обработке уведомления: {ex.Message}");
            await context.NotifyFaulted(TimeSpan.FromSeconds(1), nameof(CreateNotificationConsumer), ex);
        }
    }
}