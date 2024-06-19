using NotificationService.Models;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task CreateNotificationAsync(Notification? notification);
        Task UpdateNotificationAsync(Notification? notification);
        Task DeleteNotificationAsync(Notification? notification);
    }
}
