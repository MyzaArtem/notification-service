using NotificationService.Models;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        bool SaveChanges();
        IEnumerable<Notification> GetAllNotificationsForUser(int userID);
        Notification GetNotificationById(int id);
        void CreateNotification(Notification notification);
    }
}
