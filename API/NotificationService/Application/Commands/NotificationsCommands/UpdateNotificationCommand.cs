using Application.DTOs;
using MediatR;

namespace Application.Commands.NotificationsCommands
{
    public class UpdateNotificationCommand(NotificationUpdateDto? notification) : IRequest<ServiceResponse>
    {
        public NotificationUpdateDto? Notification { get; } = notification;
    }
}
