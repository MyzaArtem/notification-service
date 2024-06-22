using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public short Status { get; set; }
    }
}
