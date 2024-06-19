﻿using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Notification
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }

        [Required]
        public int NotificationCategoryId { get; set; }
        public NotificationCategory NotificationCategory { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }

}
