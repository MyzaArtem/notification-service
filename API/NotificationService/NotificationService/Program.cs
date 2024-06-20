using Microsoft.EntityFrameworkCore;
using NotificationService.Configuration;
using NotificationService.Models;
using NotificationService.Repositories;

namespace NotificationService
{
    //  миграции
    // dotnet ef database update 
    //  докер ексек
    // psql -U postgres -d notifications 
    //  узнать какие таблички
    // \dt
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}