using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.AsyncDataService
{
    public abstract class IMessageBusSubscriber : BackgroundService, IDisposable
    {
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(_stoppingCts.Token);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _stoppingCts.Cancel();
            return Task.CompletedTask;
        }

        public abstract void Initialize();

        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public override void Dispose()
        {
            _stoppingCts.Cancel();
            base.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stoppingCts.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
