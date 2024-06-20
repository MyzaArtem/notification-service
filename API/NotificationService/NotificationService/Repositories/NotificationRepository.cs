using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        protected NotificationRepository(DbContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(int userId)
        {
            return await _dataContext.Set<Notification>()
                                     .Where(n => n.UserId == userId)
                                     .ToListAsync();
        }
    }
}
