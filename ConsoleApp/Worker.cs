using System;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    public class Worker : IWorker
    {
        private IConfiguration _configuration { get; }

        public Worker (IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void StartWork()
        {
            Console.WriteLine("START WORK");
            Console.WriteLine($"'TMP' value from environment variables: {_configuration["TMP"]}");
            Console.WriteLine($"'environment' value from appsettings.json: {_configuration["environment"]}");
            // Can also read commandline args from IConfiguration as well: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1#default-builder-settings-1
        }

        public void StopWork()
        {
            Console.WriteLine("STOP WORK");
        }
    }
}
