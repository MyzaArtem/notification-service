using Microsoft.EntityFrameworkCore;
using NotificationService.Models;
using NotificationService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private const int InvalidID = 0;
        private readonly INotificationRepository _repository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository repository, ILogger<NotificationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId)
        {
            if (userId <= InvalidID)
            {
                _logger.LogError($"Invalid user ID: {userId}");
                throw new ArgumentException("Invalid user ID", nameof(userId));
            }

            return await _repository.GetAllNotificationsForUserAsync(userId);
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            if (id <= InvalidID)
            {
                _logger.LogError($"Invalid ID: {id}");
                throw new ArgumentException("Invalid ID", nameof(id));
            }

            var notification = await _repository.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                _logger.LogWarning($"Notification with id {id} not found.");
                throw new InvalidOperationException($"Notification with id {id} not found.");
            }
            return notification;
        }

        public async Task CreateNotificationAsync(Notification? notification)
        {
            if (notification == null)
            {
                _logger.LogError("Invalid notification: null");
                throw new ArgumentNullException("Invalid notification", nameof(notification));
            }

            await _repository.CreateNotificationAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification? notification)
        {
            if (notification == null)
            {
                _logger.LogError("Invalid notification: null");
                throw new ArgumentNullException("Invalid notification", nameof(notification));
            }

            _repository.UpdateNotificationAsync(notification);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error when trying to save update.");
                throw new DbUpdateConcurrencyException("Error when trying to save update.\r\n");
            }
        }

        public async Task DeleteNotificationAsync(Notification? notification)
        {
            if (notification == null)
            {
                _logger.LogError("Invalid notification: null");
                throw new ArgumentNullException("Invalid notification", nameof(notification));
            }

            _repository.DeleteNotificationAsync(notification);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error when trying to delete.");
                throw new DbUpdateConcurrencyException("Error when trying to delete.\r\n");
            }
        }
    }
}
