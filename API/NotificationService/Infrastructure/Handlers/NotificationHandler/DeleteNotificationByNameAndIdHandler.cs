using Application.Commands.NotificationsCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Enums;
using Application.DTOs;
using Domain.Models;

namespace Infrastructure.Handlers.NotificationHandler
{
    public class DeleteNotificationByNameAndIdHandler : IRequestHandler<DeleteNotificationByNameAndIdCommand, ServiceResponse>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<DeleteNotificationByNameAndIdHandler> _logger;

        public DeleteNotificationByNameAndIdHandler(AppDbContext appDbContext, ILogger<DeleteNotificationByNameAndIdHandler> logger)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceResponse> Handle(DeleteNotificationByNameAndIdCommand request, CancellationToken cancellationToken)
        {
            var service = await _appDbContext.Services.FirstOrDefaultAsync(s => s.Name == request.Name, cancellationToken);

            if (service == null)
            {
                _logger.LogInformation("Сервис не найден");
                return new ServiceResponse(true, "Сервис не найден");
            }

            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

            if (notification == null)
            {
                _logger.LogInformation("Уведомление не найдено");
                return new ServiceResponse(true, "Уведомление не найдено");
            }

            if (service.Id != notification.ServiceId)
            {
                _logger.LogInformation("Уведомление не принадлежит данному сервису");
                return new ServiceResponse(true, "Уведомление не принадлежит данному сервису");
            }

            notification.Status = (short)Status.Deleted;

            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Уведомление сервиса {ServiceName} с ID: {NotificationId} успешно удалено", request.Name, request.Id);

            return new ServiceResponse(true, "Уведомление успешно удалено");
        }
    }
}
