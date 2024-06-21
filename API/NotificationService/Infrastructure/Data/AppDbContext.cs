using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Domain.Models;

namespace Infrastructure.Data
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