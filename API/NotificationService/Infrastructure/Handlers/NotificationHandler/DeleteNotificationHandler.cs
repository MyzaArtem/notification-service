using Application.Commands.NotificationsCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteNotificationHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

            if (notification == null)
            {
                throw new InvalidOperationException($"Notification with ID {request.Id} not found.");
            }

            notification.Status = -1;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
