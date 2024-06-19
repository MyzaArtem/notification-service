using NotificationService.Models;

namespace NotificationService.Repositories
{
    public static class PrepareDatebase
    {
        public static async Task Prepare(
            INotificationRepository notificationRepo,
            INotificationCategoryRepository categoryRepo,
            INotificationSettingsRepository settingRepo,
            INotificationTypeRepository typeRepo,
            IUserRepository userRepo,
            IServiceRepository serviceRepo
        )
        {
            List<NotificationCategory> categories = new List<NotificationCategory>
            {
                new NotificationCategory { Id = 1, Name = "Category 1", Description = "Description for Category 1" },
                new NotificationCategory { Id = 2, Name = "Category 2", Description = "Description for Category 2" },
                new NotificationCategory { Id = 3, Name = "Category 3", Description = "Description for Category 3" }
            };

            List<NotificationType> types = new List<NotificationType>
            {
                new NotificationType { Id = 1, ServiceId = 1, TypeName = "Type 1" },
                new NotificationType { Id = 2, ServiceId = 2, TypeName = "Type 2" },
                new NotificationType { Id = 3, ServiceId = 1, TypeName = "Type 3" }
            };

            List<Service> services = new List<Service>
            {
                new Service { Id = 1, Name = "Service 1" },
                new Service { Id = 2, Name = "Service 2" },
                new Service { Id = 3, Name = "Service 3" }
            };

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@example.com" },
                new User { Id = 2, Name = "User 2", Email = "user2@example.com" },
                new User { Id = 3, Name = "User 3", Email = "user3@example.com" }
            };

            List<NotificationSettings> settings = new List<NotificationSettings>
            {
                new NotificationSettings { Id = 1, UserId = 1, EnableEmailNotifications = true, EnableSomeCategoryNotification = false },
                new NotificationSettings { Id = 2, UserId = 2, EnableEmailNotifications = false, EnableSomeCategoryNotification = true },
                new NotificationSettings { Id = 3, UserId = 3, EnableEmailNotifications = true, EnableSomeCategoryNotification = true }
            };

            foreach (var category in categories)
            {
                await categoryRepo.CreateNotificationCategoryAsync(category);
            }

            foreach (var service in services)
            {
                await serviceRepo.CreateNotificationServiceAsync(service);
            }

            foreach (var type in types)
            {
                await typeRepo.CreateNotificationTypeAsync(type);
            }

            foreach (var setting in settings)
            {
                await settingRepo.CreateNotificationSettingsAsync(setting);
            }

            foreach (var user in users)
            {
                await userRepo.CreateNotificationUserAsync(user);
            }

            await notificationRepo.SaveChangesAsync();
            await categoryRepo.SaveChangesAsync();
            await settingRepo.SaveChangesAsync();
            await typeRepo.SaveChangesAsync();
            await userRepo.SaveChangesAsync();
            await serviceRepo.SaveChangesAsync();

        }
    }
}
