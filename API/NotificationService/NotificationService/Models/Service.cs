using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Service
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }

}
