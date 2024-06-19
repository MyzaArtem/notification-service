using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class NotificationTypeProfile : Profile
    {
        public NotificationTypeProfile()
        {
            CreateMap<NotificationType, NotificationTypeReadDto>();
            CreateMap<NotificationTypeCreateDto, NotificationType>();
            CreateMap<NotificationTypeUpdateDto, NotificationType>();
        }
    }
}
