using AutoMapper;
using Application.DTOs;
using Domain.Models;

namespace Application.Profiles
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
