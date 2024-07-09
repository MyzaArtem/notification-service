using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;

namespace Domain.Models;

public class DeleteNotification : BaseEntity
{
    [Required]
    public string ServiceName { get; set; }

    public string NotificationId { get; set; }
}