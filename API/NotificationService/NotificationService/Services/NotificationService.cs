using NotificationService.Models;
using NotificationService.Repositories;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(userId));
            }

            return await _repository.GetAllNotificationsForUserAsync(userId);
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _repository.GetNotificationByIdAsync(id);
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            await _repository.CreateNotificationAsync(notification);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }
    }

}
