using Microsoft.EntityFrameworkCore;
using NotificationService.Models;

namespace NotificationService.Data
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly AppDbContext _context;

        public NotificationRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateNotification(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            _context.Platforms.Add(notification);
        }

        public IEnumerable<Notification> GetAllNotificationsForUser(int userID)
        {
            // TODO : Реализовать метод
            throw new ArgumentNullException(nameof(userID));
        }

        public Notification GetNotificationById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }

}
