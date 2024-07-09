using Application.Commands.NotificationsCommands;
using MassTransit;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Consumers;

public class UserConsumer : IConsumer<User>
{
    private readonly ILogger<UserConsumer> _logger;
    private readonly IMediator _mediator;

    public UserConsumer(ILogger<UserConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<User> context)
    {
        /* Example?
           {
              "StudentID": "00000001-0000-0000-0000-000000000001",
              "PersonId": "12340001-0000-0000-0000-000000000002",
           }
         */
        
        try
        {
            _logger.LogInformation("Получение пользователя из очереди");
            _logger.LogInformation(context.Message.StudentID.ToString());
            _logger.LogInformation(context.Message.PersonId.ToString());
            
            await context.NotifyConsumed(TimeSpan.FromSeconds(1), nameof(UserConsumer));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при получении пользователя из очереди: {ex.Message}");
            await context.NotifyFaulted(TimeSpan.FromSeconds(1), nameof(UserConsumer), ex);
        }
    }
}