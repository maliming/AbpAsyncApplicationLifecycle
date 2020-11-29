using System.Threading.Tasks;
using AbpAsyncApplicationLifecycle;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedModule;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace WebApp.Web
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcUiModule),
        typeof(MyModule)
    )]
    public class WebAppWebModule : AbpModule, IAbpAsyncApplicationLifecycle
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                app
                    .UseStatusCodePagesWithRedirects("~/Error?httpStatusCode={0}")
                    .UseExceptionHandler("/Error");
            }

            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseConfiguredEndpoints();
        }

        public Task StartAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<WebAppWebModule>>()
                .LogError("WebAppWebModule.StartAsync");

            return Task.CompletedTask;
        }

        public Task StopAsync(AbpAsyncApplicationLifecycleContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogger<WebAppWebModule>>()
                .LogError("WebAppWebModule.StopAsync");

            return Task.CompletedTask;
        }
    }
}
