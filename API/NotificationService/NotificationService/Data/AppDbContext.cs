using Microsoft.EntityFrameworkCore;
using NotificationService.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotificationService.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Notification -> User (один ко многим)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Notification -> Service (один ко многим)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Service)
                .WithMany(s => s.Notifications)
                .HasForeignKey(n => n.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Notification -> NotificationType (один ко многим)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.NotificationType)
                .WithMany(nt => nt.Notifications)
                .HasForeignKey(n => n.NotificationTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Notification -> NotificationCategory (один ко многим)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.NotificationCategory)
                .WithMany(nc => nc.Notifications)
                .HasForeignKey(n => n.NotificationCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // NotificationSettings -> User (один к одному)
            modelBuilder.Entity<NotificationSettings>()
                .HasOne(ns => ns.User)
                .WithMany(u => u.NotificationSettings)
                .HasForeignKey(ns => ns.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // NotificationType -> Service (один ко многим)
            modelBuilder.Entity<NotificationType>()
                .HasOne(nt => nt.Service)
                .WithMany(s => s.NotificationTypes)
                .HasForeignKey(nt => nt.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}