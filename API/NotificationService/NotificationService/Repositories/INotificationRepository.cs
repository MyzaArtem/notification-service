using NotificationService.Models;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        bool SaveChanges();
        IEnumerable<Notification> GetAllNotificationsForUser(int userID);
        Notification GetNotificationById(int id);
        void CreateNotification(Notification notification);
    }
}
