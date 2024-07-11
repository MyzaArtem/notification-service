using Application.Commands.NotificationsCommands;
using MassTransit;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs;

namespace API.Hubs;

public class DeleteNotificationConsumer : IConsumer<DeleteNotificationDto>
{
    private readonly ILogger<DeleteNotificationConsumer> _logger;
    private readonly IMediator _mediator;

    public DeleteNotificationConsumer(ILogger<DeleteNotificationConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<DeleteNotificationDto> context)
    {
        /* Example?
           {
             "ServiceName": "ServiceName",
             "NotificationId": "00000001-0000-0000-0000-000000000001"
           }
         */
        
        try
        {
            _logger.LogInformation("Удаление уведомления");
            var notificationReadDto = 
                await _mediator.Send(new DeleteNotificationByNameAndIdCommand(
                    context.Message.ServiceName,new Guid(context.Message.NotificationId)));
            
            await context.NotifyConsumed(TimeSpan.FromSeconds(1), nameof(DeleteNotificationConsumer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при обработке уведомления: {ex.Message}");
            await context.NotifyFaulted(TimeSpan.FromSeconds(1), nameof(DeleteNotificationConsumer), ex);
        }
    }
}