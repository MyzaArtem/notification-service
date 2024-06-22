using Domain.Models;
using MediatR;

namespace Application.Commands.NotificationsCommands
{
    public class UpdateNotificationCommand(Notification? notification) : IRequest<Guid>
    {
        public Notification? Notification { get; } = notification;
    }
}
