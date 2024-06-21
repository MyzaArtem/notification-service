namespace Application.DTOs
{
    public class NotificationReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid NotificationTypeId { get; set; }
        public Guid NotificationCategoryId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public short Status { get; set; }
    }

}
