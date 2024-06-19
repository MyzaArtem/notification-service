using NotificationService.Models;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task CreateNotificationAsync(Notification notification);
    }
}
