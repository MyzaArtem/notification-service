using Microsoft.EntityFrameworkCore;
using NotificationService.Models;
using NotificationService.Repositories;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        public const int InvalidID = 0;
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId)
        {
            if (userId <= InvalidID)
            {
                throw new ArgumentException("Invalid user ID", nameof(userId));
            }

            return await _repository.GetAllNotificationsForUserAsync(userId);
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            if (id <= InvalidID)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }

            var notification = await _repository.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                throw new InvalidOperationException($"Notification with id {id} not found.");
            }
            return notification;
        }

        public async Task CreateNotificationAsync(Notification? notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            await _repository.CreateNotificationAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification? notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            _repository.UpdateNotificationAsync(notification);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Error when trying to save update.\r\n");
            }
        }

        public async Task DeleteNotificationAsync(Notification? notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            _repository.DeleteNotificationAsync(notification);

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Error when trying to delete.\r\n");
            }
        }

    }

}
