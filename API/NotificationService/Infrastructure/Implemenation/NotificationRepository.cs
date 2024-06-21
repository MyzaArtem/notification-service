using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implemenation
{
    public class NotificationRepository : EFRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(Guid userId)
        {
            return await _dataContext.Set<Notification>()
                                     .Where(n => n.UserId == userId)
                                     .ToListAsync();
        }
    }
}
