using Application.Commands.NotificationsCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Enums;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<DeleteNotificationHandler> _logger;

        public DeleteNotificationHandler(AppDbContext appDbContext, ILogger<DeleteNotificationHandler> logger)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

            if (notification == null)
            {
                throw new InvalidOperationException($"Notification with ID {request.Id} not found.");
            }

            notification.Status = (short)Status.Deleted;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted notification with ID: {NotificationId}", request.Id);

        }
    }
}
