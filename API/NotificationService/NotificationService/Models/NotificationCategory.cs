using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationCategory
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

}
