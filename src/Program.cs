using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
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
