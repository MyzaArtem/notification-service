using NotificationService.Abstractions;
using NotificationService.Models;

namespace NotificationService.Repositories
{
    public static class PrepareDatebase
    {
        public static async Task Prepare(
            INotificationRepository notificationRepo,
            IRepository<NotificationCategory> categoryRepo,
            IRepository<NotificationSettings> settingRepo,
            IRepository<NotificationType> typeRepo,
            IRepository<User> userRepo,
            IRepository<Service> serviceRepo
        )
        {
            List<NotificationCategory> categories = new List<NotificationCategory>
            {
                new NotificationCategory { Id = Guid.NewGuid(), Name = "Category 1", Description = "Description for Category 1" },
                new NotificationCategory { Id = Guid.NewGuid(), Name = "Category 2", Description = "Description for Category 2" },
                new NotificationCategory { Id = Guid.NewGuid(), Name = "Category 3", Description = "Description for Category 3" }
            };

            List<NotificationType> types = new List<NotificationType>
            {
                new NotificationType { Id = Guid.NewGuid(), ServiceId = 1, TypeName = "Type 1" },
                new NotificationType { Id = Guid.NewGuid(), ServiceId = 2, TypeName = "Type 2" },
                new NotificationType { Id = Guid.NewGuid(), ServiceId = 1, TypeName = "Type 3" }
            };

            List<Service> services = new List<Service>
            {
                new Service { Id = Guid.NewGuid(), Name = "Service 1" },
                new Service { Id = Guid.NewGuid(), Name = "Service 2" },
                new Service { Id = Guid.NewGuid(), Name = "Service 3" }
            };

            List<User> users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "User 1", Email = "user1@example.com" },
                new User { Id = Guid.NewGuid(), Name = "User 2", Email = "user2@example.com" },
                new User { Id = Guid.NewGuid(), Name = "User 3", Email = "user3@example.com" }
            };

            List<NotificationSettings> settings = new List<NotificationSettings>
            {
                new NotificationSettings { Id = Guid.NewGuid(), UserId = 1, EnableEmailNotifications = true, EnableSomeCategoryNotification = false },
                new NotificationSettings { Id = Guid.NewGuid(), UserId = 2, EnableEmailNotifications = false, EnableSomeCategoryNotification = true },
                new NotificationSettings { Id = Guid.NewGuid(), UserId = 3, EnableEmailNotifications = true, EnableSomeCategoryNotification = true }
            };

            foreach (var category in categories)
            {
                await categoryRepo.CreateAsync(category);
            }

            foreach (var service in services)
            {
                await serviceRepo.CreateAsync(service);
            }

            foreach (var type in types)
            {
                await typeRepo.CreateAsync(type);
            }

            foreach (var setting in settings)
            {
                await settingRepo.CreateAsync(setting);
            }

            foreach (var user in users)
            {
                await userRepo.CreateAsync(user);
            }
        }
    }
}
