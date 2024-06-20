using NotificationService.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Service : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }

}
