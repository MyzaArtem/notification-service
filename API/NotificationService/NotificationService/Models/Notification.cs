using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Notification
    {
        [Key]
        [Required]
        public uint Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ServiceId { get; set; }

        [Required]
        public string NotificationTypeId { get; set; }

        [Required]
        public string NotificationCategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsRead { get; set; }

    }
}
