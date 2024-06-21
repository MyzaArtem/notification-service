using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class PrepareDatabase
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
                new NotificationCategory { Id = new Guid("00000001-0000-0000-0000-000000000001"), Name = "Category 1", Description = "Description for Category 1" },
                new NotificationCategory { Id = new Guid("00000001-0000-0000-0000-000000000002"), Name = "Category 2", Description = "Description for Category 2" },
                new NotificationCategory { Id = new Guid("00000001-0000-0000-0000-000000000003"), Name = "Category 3", Description = "Description for Category 3" }
            };


            List<Service> services = new List<Service>
            {
                new Service { Id = new Guid("00000001-0000-0000-0000-000000000001"), Name = "Service 1" },
                new Service { Id = new Guid("00000001-0000-0000-0000-000000000002"), Name = "Service 2" },
                new Service { Id = new Guid("00000001-0000-0000-0000-000000000003"), Name = "Service 3" }
            };

            List<NotificationType> types = new List<NotificationType>
            {
                new NotificationType { Id = new Guid("00000001-0000-0000-0000-000000000001"), ServiceId = services[0].Id, TypeName = "Type 1" },
                new NotificationType { Id = new Guid("00000001-0000-0000-0000-000000000002"), ServiceId = services[1].Id, TypeName = "Type 2" },
                new NotificationType { Id = new Guid("00000001-0000-0000-0000-000000000003"), ServiceId = services[2].Id, TypeName = "Type 3" }
            };

            List<User> users = new List<User>
            {
                new User { Id = new Guid("00000001-0000-0000-0000-000000000001")},
                new User { Id = new Guid("00000001-0000-0000-0000-000000000002")},
                new User { Id = new Guid("00000001-0000-0000-0000-000000000003")}
            };

            List<NotificationSettings> settings = new List<NotificationSettings>
            {
                new NotificationSettings { Id = new Guid("00000001-0000-0000-0000-000000000001"), UserId = users[0].Id, EnableEmailNotifications = true, EnableSomeCategoryNotification = false },
                new NotificationSettings { Id = new Guid("00000001-0000-0000-0000-000000000002"), UserId = users[1].Id, EnableEmailNotifications = false, EnableSomeCategoryNotification = true },
                new NotificationSettings { Id = new Guid("00000001-0000-0000-0000-000000000003"), UserId = users[2].Id, EnableEmailNotifications = true, EnableSomeCategoryNotification = true }
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


            foreach (var user in users)
            {
                await userRepo.CreateAsync(user);
            }

            foreach (var setting in settings)
            {
                await settingRepo.CreateAsync(setting);
            }
        }
    }

}
