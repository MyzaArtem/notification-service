using Application.Commands.NotificationsCommands;
using MassTransit;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.Hubs;

public class ServiceConsumer : IConsumer<Service>
{
    private readonly ILogger<ServiceConsumer> _logger;
    private readonly IMediator _mediator;

    protected readonly IServiceProvider _serviceProvider;

    public ServiceConsumer(ILogger<ServiceConsumer> logger, IMediator mediator, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
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

            var chatHub = (IHubContext<NotificationHub>)_serviceProvider.GetService(typeof(IHubContext<NotificationHub>));

            var temp = JsonConvert.SerializeObject(context.Message);

            chatHub.Clients.All.SendAsync("ReceiveServices", temp);

            await context.NotifyConsumed(TimeSpan.FromSeconds(1), nameof(ServiceConsumer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при получении сервиса из очереди: {ex.Message}");
            await context.NotifyFaulted(TimeSpan.FromSeconds(1), nameof(ServiceConsumer), ex);
        }
    }
}