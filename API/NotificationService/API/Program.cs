namespace API
{
    //  ��������
    // dotnet ef database update 
    //  ����� �����
    // psql -U postgres -d notifications 
    //  ������ ����� ��������
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