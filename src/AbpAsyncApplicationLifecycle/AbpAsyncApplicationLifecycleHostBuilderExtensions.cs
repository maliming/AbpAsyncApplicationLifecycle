using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AbpAsyncApplicationLifecycle
{
    public static class AbpAsyncApplicationLifecycleHostBuilderExtensions
    {
        public static IHostBuilder ConfigureAbpAsyncApplicationLifecycle(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddConventionalRegistrar(new AbpAsyncApplicationLifecycleConventionalRegistrar());
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
