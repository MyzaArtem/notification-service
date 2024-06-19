using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Data;
using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(AppDbContext context, ILogger<NotificationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            _logger.LogInformation($"Creating notification with ID: {notification.Id}");
            await _context.Notifications.AddAsync(notification);
            _logger.LogInformation($"Notification with ID {notification.Id} created successfully.");
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId)
        {
            _logger.LogInformation($"Fetching notifications for user with ID: {userId}");
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();
            _logger.LogInformation($"Fetched {notifications.Count} notifications for user with ID {userId}");
            return notifications;
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching notification with ID: {id}");
            var notification = await _context.Notifications.FirstOrDefaultAsync(p => p.Id == id);
            if (notification == null)
            {
                _logger.LogWarning($"Notification with ID {id} not found.");
            }
            else
            {
                _logger.LogInformation($"Notification with ID {id} fetched successfully.");
            }
            return notification;
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation("Saving changes to the database.");
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateNotificationAsync(Notification existingNotification, Notification notification)
        {
            _logger.LogInformation($"Updating notification with ID: {notification.Id}");
            _context.Entry(existingNotification).CurrentValues.SetValues(notification);
            _logger.LogInformation($"Notification with ID {notification.Id} updated successfully.");
        }

        public void DeleteNotificationAsync(Notification notification)
        {
            _logger.LogInformation($"Deleting notification with ID: {notification.Id}");
            _context.Notifications.Remove(notification);
            _logger.LogInformation($"Notification with ID {notification.Id} deleted successfully.");
        }
    }
}
