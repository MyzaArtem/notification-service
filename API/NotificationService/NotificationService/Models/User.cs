using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class User
    {
        [Key]
        [Required]
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
