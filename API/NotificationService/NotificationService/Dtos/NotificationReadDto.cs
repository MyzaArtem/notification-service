namespace NotificationService.Dtos
{

    public class NotificationReadDto
    {
        public uint Id { get; set; }

        /*public string UserId { get; set; }

        public string ServiceId { get; set; }

        public string NotificationTypeId { get; set; }

        public string NotificationCategoryId { get; set; }*/

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsRead { get; set; }
    }
}
