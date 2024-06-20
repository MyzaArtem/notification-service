using Microsoft.EntityFrameworkCore;
using NotificationService.Abstractions;
using NotificationService.Models;

namespace NotificationService.Repositories
{
    public abstract class INotificationRepository : EFRepository<Notification>
    {
        protected INotificationRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public virtual async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId)
        {
            return await _dataContext.Set<Notification>()
                                     .Where(n => n.UserId == userId)
                                     .ToListAsync();
        }
    }
}
