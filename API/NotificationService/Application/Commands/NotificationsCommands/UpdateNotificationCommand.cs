using Application.DTOs;
using MediatR;

namespace Application.Commands.NotificationsCommands
{
    public class UpdateNotificationCommand(NotificationUpdateDto? notification) : IRequest<Guid>
    {
        public NotificationUpdateDto? Notification { get; } = notification;
    }
}
