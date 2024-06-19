using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            CreateMap<Notification, NotificationReadDto>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<NotificationUpdateDto, Notification>();
        }

    }

}
