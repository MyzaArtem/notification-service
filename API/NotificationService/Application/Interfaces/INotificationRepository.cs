using Domain.Models;

namespace Application.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetAllNotificationsForUserAsync(Guid userId);
    }
}
