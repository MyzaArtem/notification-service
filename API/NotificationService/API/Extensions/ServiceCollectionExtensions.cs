using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Implemenation;
using Domain.Models;
using Infrastructure.Handlers.NotificationHandler;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using AutoMapper;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string? connection = configuration.GetConnectionString("DefaultConnection");
            if (connection == null)
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or null.");
            }

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(connection));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IRepository<NotificationCategory>, EFRepository<NotificationCategory>>();
            services.AddScoped<IRepository<NotificationSettings>, EFRepository<NotificationSettings>>();
            services.AddScoped<IRepository<NotificationType>, EFRepository<NotificationType>>();
            services.AddScoped<IRepository<User>, EFRepository<User>>();
            services.AddScoped<IRepository<Service>, EFRepository<Service>>();

            services.AddScoped<EFRepository<Notification>>();

            services.AddScoped<IRepository<Notification>>(provider => provider.GetRequiredService<EFRepository<Notification>>());
            services.AddScoped<IQuerier<Notification>>(provider => provider.GetRequiredService<EFRepository<Notification>>());


            return services;
        }

        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllNotificationsForUserHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetNotificationByIdHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateNotificationHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateNotificationHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteNotificationHandler).Assembly));

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Notification API", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddControllers();

            return services;
        }
    }
}
