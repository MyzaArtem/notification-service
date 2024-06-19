using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class NotificationCategoryProfile : Profile
    {
        public NotificationCategoryProfile()
        {
            CreateMap<NotificationCategory, NotificationCategoryReadDto>();
            CreateMap<NotificationCategoryCreateDto, NotificationCategory>();
            CreateMap<NotificationCategoryUpdateDto, NotificationCategory>();
        }
    }
}
