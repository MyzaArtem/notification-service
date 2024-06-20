using Microsoft.EntityFrameworkCore;
using NotificationService.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotificationService.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationCategory> NotificationCategories { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}