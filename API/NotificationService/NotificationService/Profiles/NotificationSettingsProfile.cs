using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class NotificationSettingsProfile : Profile
    {
        public NotificationSettingsProfile()
        {
            CreateMap<NotificationSettings, NotificationSettingsReadDto>();
            CreateMap<NotificationSettingsCreateDto, NotificationSettings>();
            CreateMap<NotificationSettingsUpdateDto, NotificationSettings>();
        }
    }
}
