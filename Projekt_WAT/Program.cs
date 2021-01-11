using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

    // Aplikacja do monitorowania przyjmowania leków i wyœwietlania skutecznoœci 
    // dzia³ania terapii w kontekœcie wype³nianych 5 razy dziennie ankiet pacjenta o 
    // symptomach rejestrowanych w postaci pytañ

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
