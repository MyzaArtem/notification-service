using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<NotificationSettings> NotificationSettings { get; set; } = new List<NotificationSettings>();
    }
}
