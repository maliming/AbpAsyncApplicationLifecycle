using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Modularity;

namespace AbpAsyncApplicationLifecycle
{
    public static class AbpAsyncApplicationLifecycleHostBuilderExtensions
    {
        public static IHostBuilder AddAbpAsyncApplicationLifecycle<TStartupModule>(this IHostBuilder hostBuilder)
            where TStartupModule : IAbpModule
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddConventionalRegistrar(new AbpAsyncApplicationLifecycleConventionalRegistrar());
                services.Configure<AbpAsyncApplicationLifecycleOptions>(options =>
                {
                    options.StartupModule = typeof(TStartupModule);
                });
            });
        }

        public static IHostBuilder UseAbpAsyncApplicationLifecycle(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<IAbpAsyncApplicationLifecycleServiceProvider, ModuleSortedAbpAsyncApplicationLifecycleServiceProvider>();
                services.AddHostedService<AbpAsyncApplicationLifecycleHostedService>();
            });
        }
    }
}
