using Application.Commands.NotificationsCommands;
using Application.DTOs;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, ServiceResponse>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UpdateNotificationHandler> _logger;

        public UpdateNotificationHandler(AppDbContext appDbContext, ILogger<UpdateNotificationHandler> logger)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceResponse> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            if (request.Notification == null)
            {
                return new ServiceResponse(true, "Уведомление равно null");
            }

            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.Notification.Id, cancellationToken);

            if (notification == null)
            {
                return new ServiceResponse(true, "Уведомление не найдено");
            }

            notification.Title = request.Notification.Title;
            notification.Message = request.Notification.Message;
            notification.ReadAt = request.Notification.ReadAt;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Уведомление с ID: {NotificationId} успешно обновлено", notification.Id);

            return new ServiceResponse(true, "Уведомление успешно обновлено");
        }
    }
}
