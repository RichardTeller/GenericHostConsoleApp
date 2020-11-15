using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

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
