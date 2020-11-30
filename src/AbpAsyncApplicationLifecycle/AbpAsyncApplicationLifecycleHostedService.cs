using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace AbpAsyncApplicationLifecycle
{
    public class AbpAsyncApplicationLifecycleHostedService : IHostedService
    {
        private readonly IAbpApplication _abpApplication;

        public AbpAsyncApplicationLifecycleHostedService(IAbpApplication abpApplication)
        {
            _abpApplication = abpApplication;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            EnsureServiceProviderInitialized();

            var abpAsyncInitializeServices = _abpApplication.ServiceProvider
                .GetRequiredService<IAbpAsyncApplicationLifecycleServiceProvider>()
                .GetServices(_abpApplication.ServiceProvider, _abpApplication.StartupModuleType);

            if (abpAsyncInitializeServices != null)
            {
                foreach (var asyncInitializeService in abpAsyncInitializeServices)
                {
                    //TODO: Handler exception?
                    await asyncInitializeService.StartAsync(new AbpAsyncApplicationLifecycleContext(_abpApplication.ServiceProvider));
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            EnsureServiceProviderInitialized();

            var abpAsyncInitializeServices = _abpApplication.ServiceProvider
                .GetRequiredService<IAbpAsyncApplicationLifecycleServiceProvider>()
                .GetServices(_abpApplication.ServiceProvider, _abpApplication.StartupModuleType);

            if (abpAsyncInitializeServices != null)
            {
                foreach (var asyncInitializeService in abpAsyncInitializeServices)
                {
                    //TODO: Handler exception?
                    await asyncInitializeService.StopAsync(new AbpAsyncApplicationLifecycleContext(_abpApplication.ServiceProvider));
                }
            }
        }

        private void EnsureServiceProviderInitialized()
        {
            if (_abpApplication.ServiceProvider == null)
            {
                throw new AbpException($"The {nameof(_abpApplication.ServiceProvider)} of {nameof(IAbpApplication)} has not been initialized!");
            }
        }
    }
}
