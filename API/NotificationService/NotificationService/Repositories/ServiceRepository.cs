using NotificationService.Data;
using NotificationService.Models;

namespace NotificationService.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NotificationRepository> _logger;

        public ServiceRepository(AppDbContext context, ILogger<NotificationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateNotificationServiceAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
