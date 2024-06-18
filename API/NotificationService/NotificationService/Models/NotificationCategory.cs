using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationCategory
    {
        [Key]
        [Required]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
