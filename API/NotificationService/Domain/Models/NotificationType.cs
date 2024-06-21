using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class NotificationType : BaseEntity
    {
        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

        [Required]
        public string TypeName { get; set; }
    }
}
