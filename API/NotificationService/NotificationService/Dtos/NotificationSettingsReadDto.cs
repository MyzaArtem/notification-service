﻿namespace NotificationService.Dtos
{
    public class NotificationSettingsReadDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }
    }

}