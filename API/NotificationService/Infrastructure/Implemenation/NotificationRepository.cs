using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implemenation
{
    public class NotificationRepository : INotificationRepository
    {
        private IRepository<Notification> eFRepository;
        private IQuerier<Notification> iQuerier;

        public NotificationRepository(IRepository<Notification> eFRepository, IQuerier<Notification> iQuerier)
        {
            this.eFRepository = eFRepository;
            this.iQuerier = iQuerier;
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
            return await iQuerier.Query()
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
