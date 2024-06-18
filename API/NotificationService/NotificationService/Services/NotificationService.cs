using NotificationService.Models;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService() { }

        public void CreateNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> GetAllNotificationsForUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Notification GetNotificationById(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
