using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationSettings
    {
        [Key]
        [Required]
        public uint Id { get; set; }
        public string UserId { get; set; }
        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }

    }
}
