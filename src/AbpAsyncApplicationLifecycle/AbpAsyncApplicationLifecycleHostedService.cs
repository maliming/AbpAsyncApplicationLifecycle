using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AbpAsyncApplicationLifecycle
{
    public class AbpAsyncApplicationLifecycleHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAbpAsyncApplicationLifecycleServiceProvider _asyncApplicationLifecycleServiceProvider;

        public AbpAsyncApplicationLifecycleHostedService(
            IAbpAsyncApplicationLifecycleServiceProvider asyncApplicationLifecycleServiceProvider,
            IServiceProvider serviceProvider)
        {
            _asyncApplicationLifecycleServiceProvider = asyncApplicationLifecycleServiceProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var abpAsyncInitializeServices = _asyncApplicationLifecycleServiceProvider.GetServices(_serviceProvider);
            if (abpAsyncInitializeServices != null)
            {
                foreach (var asyncInitializeService in abpAsyncInitializeServices)
                {
                    //TODO: Handler exception?
                    await asyncInitializeService.StartAsync(new AbpAsyncApplicationLifecycleContext(_serviceProvider));
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            var abpAsyncInitializeServices = _asyncApplicationLifecycleServiceProvider.GetServices(_serviceProvider);
            if (abpAsyncInitializeServices != null)
            {
                foreach (var asyncInitializeService in abpAsyncInitializeServices)
                {
                    //TODO: Handler exception?
                    await asyncInitializeService.StopAsync(new AbpAsyncApplicationLifecycleContext(_serviceProvider));
                }
            }
        }
    }
}
