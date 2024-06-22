using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Implemenation;
using Infrastructure.Data;
using Domain.Models;
using Infrastructure.Handlers.NotificationHandler;
using System.Reflection;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string? connection = Configuration.GetConnectionString("DefaultConnection");
            if (connection == null)
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or null.");
            }

            services.AddLogging();
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(connection));

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IRepository<NotificationCategory>, EFRepository<NotificationCategory>>();
            services.AddScoped<IRepository<NotificationSettings>, EFRepository<NotificationSettings>>();
            services.AddScoped<IRepository<NotificationType>, EFRepository<NotificationType>>();
            services.AddScoped<IRepository<User>, EFRepository<User>>();
            services.AddScoped<IRepository<Service>, EFRepository<Service>>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllNotificationsForUserHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetNotificationByIdHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateNotificationHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateNotificationHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteNotificationHandler).Assembly));
            //serilog 
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Notification API", Version = "v1" });
            });
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommandsService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();

                await PrepareDatabase.Prepare(
                    serviceScope.ServiceProvider.GetService<INotificationRepository>(),
                    serviceScope.ServiceProvider.GetService<IRepository<NotificationCategory>>(),
                    serviceScope.ServiceProvider.GetService<IRepository<NotificationSettings>>(),
                    serviceScope.ServiceProvider.GetService<IRepository<NotificationType>>(),
                    serviceScope.ServiceProvider.GetService<IRepository<User>>(),
                    serviceScope.ServiceProvider.GetService<IRepository<Service>>()
                );
            }
        }
    }
}
