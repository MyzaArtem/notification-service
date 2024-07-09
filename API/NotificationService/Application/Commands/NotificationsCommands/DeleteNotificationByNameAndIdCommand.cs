using Application.DTOs;
using MediatR;
using System.Runtime.CompilerServices;

namespace Application.Commands.NotificationsCommands
{
    public class DeleteNotificationByNameAndIdCommand(string name, Guid id) : IRequest<ServiceResponse>
    {
        public string Name { get; set; } = name;
        public Guid Id { get; set; } = id;
    }
}
