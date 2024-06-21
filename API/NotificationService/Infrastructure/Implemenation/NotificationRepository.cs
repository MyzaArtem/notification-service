using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implemenation
{
    public class NotificationRepository : INotificationRepository
    {
        private EFRepository<Notification> eFRepository;

        public NotificationRepository(AppDbContext dataContext)
        {
            eFRepository = new EFRepository<Notification>(dataContext);
        }

        public Task CreateAsync(Notification entity)
        {
            return eFRepository.CreateAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return eFRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Notification>> GetAllAsync()
        {
            return eFRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(Guid userId)
        {
            return await eFRepository.Query()
                                     .Where(n => n.UserId == userId)
                                     .ToListAsync();
        }

        public Task<Notification?> GetAsync(Guid id)
        {
            return eFRepository.GetAsync(id);
        }

        public Task UpdateAsync(Notification entity)
        {
            return eFRepository.UpdateAsync(entity);
        }
    }
}
