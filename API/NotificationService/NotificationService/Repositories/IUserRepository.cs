namespace NotificationService.Repositories
{
    public interface IUserRepository
    {
        Task<bool> SaveChangesAsync();
        Task CreateNotificationUserAsync(NotificationService.Models.User user);
    }
}
