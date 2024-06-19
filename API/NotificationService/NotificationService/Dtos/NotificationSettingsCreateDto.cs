namespace NotificationService.Dtos
{
    public class NotificationSettingsCreateDto
    {
        public int UserId { get; set; }
        public bool EnableEmailNotifications { get; set; }
        public bool EnableSomeCategoryNotification { get; set; }
    }

}
