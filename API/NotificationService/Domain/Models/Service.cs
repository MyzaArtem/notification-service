using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Service : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
