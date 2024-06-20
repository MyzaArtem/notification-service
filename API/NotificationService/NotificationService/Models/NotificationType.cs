using NotificationService.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationType : BaseEntity
    {
        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public string TypeName { get; set; }

    }

}
