using AutoMapper;
using NotificationService.Models;
using NotificationService.Dtos;

namespace NotificationService.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceReadDto>();
            CreateMap<ServiceCreateDto, Service>();
            CreateMap<ServiceUpdateDto, Service>();
        }
    }
}
