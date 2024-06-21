using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;

namespace Domain.Models
{
    public class Notification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

        [Required]
        public Guid NotificationTypeId { get; set; }

        [ForeignKey(nameof(NotificationTypeId))]
        public NotificationType NotificationType { get; set; }

        [Required]
        public Guid NotificationCategoryId { get; set; }
        [ForeignKey(nameof(NotificationCategoryId))]
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
    }
}
