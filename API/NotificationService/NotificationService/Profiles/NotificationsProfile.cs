﻿using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class NotificationsProfile : Profile
    {
        public NotificationsProfile()
        {
            CreateMap<NotificationCategory, NotificationCategoryReadDto>();
            CreateMap<NotificationCategoryCreateDto, NotificationCategory>();
            CreateMap<NotificationCategoryUpdateDto, NotificationCategory>();

            CreateMap<Notification, NotificationReadDto>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<NotificationUpdateDto, Notification>();
        }

    }

}
