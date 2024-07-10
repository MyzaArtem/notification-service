using API.Extensions;
using API.Hubs;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Consumers;
using Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services
                .AddDatabaseConfiguration(Configuration)
                .AddRepositories()
                .AddMediatRHandlers()
                .AddSwaggerConfiguration()
                .AddCustomServices()
                .AddRabbitMQService();
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
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notification-hub");
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
