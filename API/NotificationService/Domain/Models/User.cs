using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        [Required]
        public Guid StudentID { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
