using System;
using System.Threading.Tasks;
using AbpAsyncApplicationLifecycle;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Modularity;

namespace SharedModule
{
    public class MyModule : AbpModule, IAbpAsyncApplicationLifecycle
    {
        public Task StartAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<MyModule>>()
                .LogError("MyModule.StartAsync");

            return Task.CompletedTask;
        }

        public Task StopAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<MyModule>>()
                .LogError("MyModule.StopAsync");

            return Task.CompletedTask;
        }
    }
}
