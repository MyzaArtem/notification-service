using NotificationService.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models
{
    public class NotificationType : BaseEntity
    {
        [Required]
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Required]
        public bool RequiresAction { get; set; }
    }


}
