using Application.Interfaces;
using Domain.Models;

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
                new NotificationCategory {
                    Id = new Guid("00000001-0000-0000-0000-000000000001"),
                    Name = "Category 1",
                    Description = "Description for Category 1",
                    Status = 1,
                },
                new NotificationCategory {
                    Id = new Guid("00000001-0000-0000-0000-000000000002"),
                    Name = "Category 2",
                    Description = "Description for Category 2",
                    Status = 1,
                },
                new NotificationCategory {
                    Id = new Guid("00000001-0000-0000-0000-000000000003"),
                    Name = "Category 3",
                    Description = "Description for Category 3",
                    Status = 1,
                }
            };


            List<Service> services = new List<Service>
            {
                new Service { Id = new Guid("00000001-0000-0000-0000-000000000001"), Name = "Service 1",Status = 1, },
                new Service { Id = new Guid("00000001-0000-0000-0000-000000000002"), Name = "Service 2",Status = 1, },
                new Service { Id = new Guid("00000001-0000-0000-0000-000000000003"), Name = "Service 3",Status = 1, }
            };

            List<NotificationType> types = new List<NotificationType>
            {
                new NotificationType { Id = new Guid("00000001-0000-0000-0000-000000000001"), ServiceId = services[0].Id, TypeName = "Type 1",Status = 1, },
                new NotificationType { Id = new Guid("00000001-0000-0000-0000-000000000002"), ServiceId = services[1].Id, TypeName = "Type 2",Status = 1, },
                new NotificationType { Id = new Guid("00000001-0000-0000-0000-000000000003"), ServiceId = services[2].Id, TypeName = "Type 3",Status = 1, }
            };

            List<User> users = new List<User>
            {
                new User { Id = new Guid("00000001-0000-0000-0000-000000000001"), Status = 1,},
                new User { Id = new Guid("00000001-0000-0000-0000-000000000002"), Status = 1,},
                new User { Id = new Guid("00000001-0000-0000-0000-000000000003"), Status = 1,}
            };

            List<NotificationSettings> settings = new List<NotificationSettings>
            {
                new NotificationSettings {
                    Id = new Guid("00000001-0000-0000-0000-000000000001"),
                    UserId = users[0].Id,
                    EnableEmailNotifications = true,
                    EnableSomeCategoryNotification = false,
                    Status = 1,
                },
                new NotificationSettings {
                    Id = new Guid("00000001-0000-0000-0000-000000000002"),
                    UserId = users[1].Id,
                    EnableEmailNotifications = false,
                    EnableSomeCategoryNotification = true,
                    Status = 1,
                },
                new NotificationSettings {
                    Id = new Guid("00000001-0000-0000-0000-000000000003"),
                    UserId = users[2].Id,
                    EnableEmailNotifications = true,
                    EnableSomeCategoryNotification = true,
                    Status = 1,
                }
            };

            List<Notification> notifications = new List<Notification>
            {
                new Notification {
                    Id = new Guid("00000001-0000-0000-0000-000000000001"),
                    NotificationCategoryId = categories[0].Id,
                    CreatedAt = DateTime.UtcNow ,
                    NotificationTypeId = types[0].Id,
                    ServiceId = services[0].Id,
                    Title = "Tittle 1",
                    Message = "Message 1",
                    ReadAt = DateTime.UtcNow,
                    Status = 1,
                    //NotificationCategory = categories[0],
                    //NotificationType = types[0],
                    //Service =services[0],
                    UserId = users[0].Id,
                    //User = users[0],
                },
                new Notification {
                    Id = new Guid("00000001-0000-0000-0000-000000000002"),
                    NotificationCategoryId = categories[1].Id,
                    CreatedAt = DateTime.UtcNow ,
                    NotificationTypeId = types[1].Id,
                    ServiceId = services[1].Id,
                    Title = "Tittle 2",
                    Message = "Message 2",
                    ReadAt = DateTime.UtcNow,
                    Status = 1,
                    //NotificationCategory = categories[1],
                    //NotificationType = types[1],
                    //Service =services[1],
                    UserId = users[1].Id,
                    //User = users[1],
                },
                new Notification {
                    Id = new Guid("00000001-0000-0000-0000-000000000003"),
                    NotificationCategoryId = categories[2].Id,
                    CreatedAt = DateTime.UtcNow ,
                    NotificationTypeId = types[2].Id,
                    ServiceId = services[2].Id,
                    Title = "Tittle 3",
                    Message = "Message 3",
                    ReadAt = DateTime.UtcNow,
                    Status = 1,
                    //NotificationCategory = categories[2],
                    //NotificationType = types[2],
                    //Service =services[2],
                    UserId = users[2].Id,
                    //User = users[2],
                },
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

            foreach (var notification in notifications)
            {
                await notificationRepo.CreateAsync(notification);
            }
        }
    }

}
