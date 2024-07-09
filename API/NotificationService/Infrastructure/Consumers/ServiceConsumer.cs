using Application.Commands.NotificationsCommands;
using MassTransit;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Consumers;

public class ServiceConsumer : IConsumer<Service>
{
    private readonly ILogger<ServiceConsumer> _logger;
    private readonly IMediator _mediator;

    public ServiceConsumer(ILogger<ServiceConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<Service> context)
    {
        /* Example?
           {
              "Name": "ServicName",
           }
         */
        
        try
        {
            _logger.LogInformation("Получение сервиса из очереди");
            _logger.LogInformation(context.Message.Name);
            
            await context.NotifyConsumed(TimeSpan.FromSeconds(1), nameof(ServiceConsumer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при получении сервиса из очереди: {ex.Message}");
            await context.NotifyFaulted(TimeSpan.FromSeconds(1), nameof(ServiceConsumer), ex);
        }
    }
}