using NotificationService.Models;

namespace NotificationService.Repositories
{
    public interface INotificationTypeRepository
    {
        Task<bool> SaveChangesAsync();
        Task CreateNotificationTypeAsync(NotificationType notificationType);
    }
}
