using NotificationService.Models;

namespace NotificationService.Data
{
    public interface INotificationRepo
    {
        bool SaveChanges();
        IEnumerable<Notification> GetAllNotificationsForUser(int userID);
        Notification GetNotificationById(int id);
        void CreateNotification(Notification notification);
    }
}
