using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            // Source -> Target
            CreateMap<Notification, NotificationReadDto>();
            CreateMap<NotificationCreateDto, Notification>();

        }
    }

}
