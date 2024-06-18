using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Service
    {
        [Key]
        [Required]
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
