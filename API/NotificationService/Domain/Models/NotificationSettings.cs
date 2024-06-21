using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Abstractions;

namespace Domain.Models
{
    public class NotificationSettings : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }
    }
}
