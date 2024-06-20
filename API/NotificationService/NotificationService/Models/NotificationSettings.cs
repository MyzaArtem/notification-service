using NotificationService.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationSettings : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }
    }

}
