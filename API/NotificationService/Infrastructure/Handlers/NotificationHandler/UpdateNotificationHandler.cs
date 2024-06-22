using Application.Commands.NotificationsCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, Guid>
    {
        private readonly AppDbContext _appDbContext;
        public UpdateNotificationHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.Notification == null)
            {
                throw new ArgumentNullException(nameof(request.Notification));
            }
            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.Notification.Id, cancellationToken);

            if (notification == null)
            {
                throw new InvalidOperationException($"Notification with ID {request.Notification.Id} not found.");
            }

            notification.Title = request.Notification.Title;
            notification.Message = request.Notification.Message;
            notification.UserId = request.Notification.UserId;
            notification.ReadAt = request.Notification.ReadAt;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return notification.Id;
        }
    }
}
