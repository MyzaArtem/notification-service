using NotificationService.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models
{
    public class Notification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [Required]
        public Guid NotificationTypeId { get; set; }
        [ForeignKey("NotificationTypeId")]
        public NotificationType NotificationType { get; set; }

        [Required]
        public Guid NotificationCategoryId { get; set; }
        [ForeignKey("NotificationCategoryId")]
        public NotificationCategory NotificationCategory { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ReadAt { get; set; }

        [Required]
        public short Status { get; set; }

        [Required]
        public bool RequiresAction { get; set; }
    }


}
