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

        public async Task CreateAsync(Notification entity)
        {
            await eFRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await eFRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await eFRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(Guid userId)
        {
            return await eFRepository.Query()
                                     .Where(n => n.UserId == userId)
                                     .ToListAsync();
        }

        public async Task<Notification?> GetAsync(Guid id)
        {
            return await eFRepository.GetAsync(id);
        }

        public async Task UpdateAsync(Notification entity)
        {
            await eFRepository.UpdateAsync(entity);
        }
    }
}
