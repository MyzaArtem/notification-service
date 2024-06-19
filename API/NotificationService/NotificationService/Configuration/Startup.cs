using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotificationService.Data;
using NotificationService.Repositories;
using NotificationService.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NotificationService.Configuration
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

            services.AddScoped<INotificationService, NotificationServiceImpl>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationCategoryRepository, NotificationCategoryRepository>();
            services.AddScoped<INotificationSettingsRepository, NotificationSettingsRepository>();
            services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });
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

                await PrepareDatebase.Prepare(
                    serviceScope.ServiceProvider.GetService<INotificationRepository>(),
                    serviceScope.ServiceProvider.GetService<INotificationCategoryRepository>(),
                    serviceScope.ServiceProvider.GetService<INotificationSettingsRepository>(),
                    serviceScope.ServiceProvider.GetService<INotificationTypeRepository>(),
                    serviceScope.ServiceProvider.GetService<IUserRepository>(),
                    serviceScope.ServiceProvider.GetService<IServiceRepository>()
                );
            }
        }
    }
}
