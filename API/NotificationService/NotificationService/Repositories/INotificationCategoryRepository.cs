namespace NotificationService.Repositories
{
    public interface INotificationCategoryRepository
    {
        Task<bool> SaveChangesAsync();
        Task CreateNotificationCategoryAsync(NotificationService.Models.NotificationCategory notificationCategory);
    }
}
