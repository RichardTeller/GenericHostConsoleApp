using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
    // This console app was built from understanding: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
    // and with some influence from: https://dfederm.com/building-a-console-app-with-.net-generic-host/
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // Add IHostedService
                    services.AddHostedService<ConsoleHostedService>();

                    // Add services
                    services.AddSingleton<IWorker, Worker>();
                });
    }
}
