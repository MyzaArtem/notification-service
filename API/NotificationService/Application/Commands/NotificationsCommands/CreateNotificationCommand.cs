using Domain.Models;
using MediatR;

namespace Application.Commands.NotificationsCommands
{
    public class CreateNotificationCommand(Notification? notification) : IRequest<Guid>
    {
        public Notification? notification { get; set; } = notification;
    }
}
