namespace Application.DTOs
{
    public class NotificationUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? ReadAt { get; set; }
        public short Status { get; set; }
    }
}
