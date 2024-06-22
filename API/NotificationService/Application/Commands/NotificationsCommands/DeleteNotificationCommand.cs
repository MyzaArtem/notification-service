using MediatR;
using System.Runtime.CompilerServices;

namespace Application.Commands.NotificationsCommands
{
    public class DeleteNotificationCommand(Guid id) : IRequest
    {
        public Guid Id { get; set; } = id;
    }
}
