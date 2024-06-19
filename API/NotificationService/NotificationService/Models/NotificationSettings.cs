using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationSettings
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }
    }

}
