using Application.Commands.NotificationsCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Enums;
using Application.DTOs;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, ServiceResponse>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<DeleteNotificationHandler> _logger;

        public DeleteNotificationHandler(AppDbContext appDbContext, ILogger<DeleteNotificationHandler> logger)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

            if (notification == null)
            {
                return new ServiceResponse(true, "Уведомление не найдено");
            }

            notification.Status = (short)Status.Deleted;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Уведомление с ID: {NotificationId} успешно удалено", request.Id);

            return new ServiceResponse(true, "Уведомление успешно удалено");

        }
    }
}
