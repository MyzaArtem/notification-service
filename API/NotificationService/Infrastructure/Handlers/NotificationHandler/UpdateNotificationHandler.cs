using Application.Commands.NotificationsCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, Guid>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UpdateNotificationHandler> _logger;

        public UpdateNotificationHandler(AppDbContext appDbContext, ILogger<UpdateNotificationHandler> logger)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            notification.ReadAt = request.Notification.ReadAt;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated notification with ID: {NotificationId}", notification.Id);

            return notification.Id;
        }
    }
}
