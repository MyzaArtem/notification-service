using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;

namespace Domain.Models
{
    public class NotificationCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
