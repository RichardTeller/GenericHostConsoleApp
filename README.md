# GenericHostConsoleApp

This project was done as an exercise while I was reading through the ASP .NET Core documentation in the [host section](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1).  I realized the generic host didn't have to be used for an ASP .NET project, and by using the generic host you can take advantage of the Microsoft Dependency Injection framework, the IConfiguration framework, and others in the `Microsoft.Extensions.X` namespace. To me this meant "Hey! This means I should be able to make a console app using this generic host, and then be able to take advantage of dependency injection, configuration, logging, etc."

*So I did just that.* This repository is a simple implementation of a generic hosted console app, as a proof of concept. At times I got some help from: https://dfederm.com/building-a-console-app-with-.net-generic-host/

This was a fun little project.

## Key Concept: IHostedService
As per the [MSDN documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1#host-definition-1): "When a host starts, it calls IHostedService.StartAsync on each implementation of IHostedService registered in the service container's collection of hosted services." Thus, any `IHostedService.StarAsync()` is effectively acting as an entry point into the application.

**The `IHostedService` interface:**
```csharp
namespace Microsoft.Extensions.Hosting
{
    //
    // Summary:
    //     Defines methods for objects that are managed by the host.
    public interface IHostedService
    {
        //
        // Summary:
        //     Triggered when the application host is ready to start the service.
        Task StartAsync(CancellationToken cancellationToken);
        //
        // Summary:
        //     Triggered when the application host is performing a graceful shutdown.
        Task StopAsync(CancellationToken cancellationToken);
    }
}
```
**My hosted service implementation:**
```csharp
namespace ConsoleApp
{
    public class ConsoleHostedService : IHostedService
    {
        private IWorker _worker { get; }

        public ConsoleHostedService(IWorker worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _worker.StartWork();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _worker.StopWork();

            return Task.CompletedTask;
        }
    }
}
```

**Adding my hosted service to the host's service container:**
```csharp
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
```

Essentially I am using my [ConsoleHostedService](https://github.com/RichardTeller/GenericHostConsoleApp/tree/main/src/ConsoleHostedService.cs) as an entry point into the console application logic. If you wanted, you could have multiple hosted services added to the host's service container in order to spin up several things at once.
