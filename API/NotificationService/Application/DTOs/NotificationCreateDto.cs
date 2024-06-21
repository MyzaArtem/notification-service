namespace Application.DTOs
{
    public class NotificationCreateDto
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid NotificationTypeId { get; set; }
        public Guid NotificationCategoryId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public short Status { get; set; }
    }

}
