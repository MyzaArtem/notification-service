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
                Console.WriteLine("Connection is null");
                return;
            }
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(connection));

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommandsService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
