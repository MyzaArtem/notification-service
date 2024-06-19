using NotificationService.Models;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task CreateNotificationAsync(Notification notification);
    }

}
