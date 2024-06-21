namespace Application.DTOs
{
    public class NotificationCreateDto
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int NotificationTypeId { get; set; }
        public int NotificationCategoryId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }

}
