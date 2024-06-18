using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationType
    {
        [Key]
        [Required]
        public uint Id { get; set; }
        public string ServiceId { get; set; }
        public string TypeName { get; set; }

    }
}
