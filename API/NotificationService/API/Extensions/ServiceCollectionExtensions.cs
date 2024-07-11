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
using MassTransit;
using Infrastructure.Consumers;

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

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy => policy
                        .WithOrigins("http://localhost:8451")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            //services.AddScoped<RabbitMqListener>();
            services.AddSingleton<IRabbitMqListener, RabbitMqListener>();

            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddRabbitMQService(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateNotificationConsumer>();
                x.AddConsumer<DeleteNotificationConsumer>();
                x.AddConsumer<UserConsumer>();
                x.AddConsumer<ServiceConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });

                    cfg.ReceiveEndpoint("CreateQueue", e =>
                    {
                        e.ConfigureConsumer<CreateNotificationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("DeleteQueue", e =>
                    {
                        e.ConfigureConsumer<DeleteNotificationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("UserQueue", e =>
                    {
                        e.ConfigureConsumer<UserConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("ServiceQueue", e =>
                    {
                        e.ConfigureConsumer<ServiceConsumer>(context);
                    });

                    cfg.ClearSerialization();
                    cfg.UseRawJsonSerializer();
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }

    }
}
