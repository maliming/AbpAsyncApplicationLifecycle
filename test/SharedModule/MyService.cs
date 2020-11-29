using System;
using System.Threading.Tasks;
using AbpAsyncApplicationLifecycle;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public class MyService : IAbpAsyncApplicationLifecycle
    {
        public Task StartAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<MyService>>()
                .LogError("MyService.StartAsync");

            return Task.CompletedTask;
        }

        public Task StopAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<MyService>>()
                .LogError("MyService.StopAsync");

            return Task.CompletedTask;
        }
    }
}
