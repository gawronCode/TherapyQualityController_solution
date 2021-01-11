using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

    // Aplikacja do monitorowania przyjmowania lek�w i wy�wietlania skuteczno�ci 
    // dzia�ania terapii w kontek�cie wype�nianych 5 razy dziennie ankiet pacjenta o 
    // symptomach rejestrowanych w postaci pyta�

namespace TherapyQualityController
{
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
