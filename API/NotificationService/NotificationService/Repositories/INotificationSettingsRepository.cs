using NotificationService.Models;

namespace NotificationService.Repositories
{
    public interface INotificationSettingsRepository
    {
        Task<bool> SaveChangesAsync();
        Task CreateNotificationSettingsAsync(NotificationService.Models.NotificationSettings notificationSettings);
    }
}
