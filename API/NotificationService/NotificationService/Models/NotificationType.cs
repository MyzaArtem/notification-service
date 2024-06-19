using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public string TypeName { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

}
