using NotificationService.Data;
using NotificationService.Models;

namespace NotificationService.Repositories
{
    public class NotificationCategoryRepository : INotificationCategoryRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationCategoryRepository(AppDbContext context, ILogger<NotificationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateNotificationCategoryAsync(NotificationCategory notificationCategory)
        {
            await _context.NotificationCategories.AddAsync(notificationCategory);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
