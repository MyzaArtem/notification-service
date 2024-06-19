namespace NotificationService.Repositories
{
    public interface IServiceRepository
    {
        Task<bool> SaveChangesAsync();
        Task CreateNotificationServiceAsync(NotificationService.Models.Service service);
    }
}
