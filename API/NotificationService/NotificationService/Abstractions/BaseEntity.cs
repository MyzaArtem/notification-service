using System.ComponentModel.DataAnnotations;

namespace NotificationService.Abstractions
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public bool RequiresAction { get; set; }

    }
}
