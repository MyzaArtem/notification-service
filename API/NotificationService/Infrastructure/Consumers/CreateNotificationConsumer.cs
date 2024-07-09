using Application.Commands.NotificationsCommands;
using MassTransit;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Consumers;

public class CreateNotificationConsumer : IConsumer<Notification>
{
    private readonly ILogger<CreateNotificationConsumer> _logger;
    private readonly IMediator _mediator;

    public CreateNotificationConsumer(ILogger<CreateNotificationConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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
            
            await context.NotifyConsumed(TimeSpan.FromSeconds(1), nameof(CreateNotificationConsumer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при обработке уведомления: {ex.Message}");
            await context.NotifyFaulted(TimeSpan.FromSeconds(1), nameof(CreateNotificationConsumer), ex);
        }
    }
}