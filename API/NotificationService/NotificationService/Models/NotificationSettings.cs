using NotificationService.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models
{
    public class NotificationSettings : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }
    }

}
