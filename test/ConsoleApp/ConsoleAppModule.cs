using System;
using System.Threading.Tasks;
using AbpAsyncApplicationLifecycle;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedModule;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ConsoleApp
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MyModule)
    )]
    public class ConsoleAppModule : AbpModule, IAbpAsyncApplicationLifecycle
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHostedService<ConsoleAppHostedService>();
        }

        public Task StartAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<ConsoleAppModule>>()
                .LogError("ConsoleAppModule.StartAsync");

            return Task.CompletedTask;
        }

        public Task StopAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<ConsoleAppModule>>()
                .LogError("ConsoleAppModule.StopAsync");

            return Task.CompletedTask;
        }
    }
}
