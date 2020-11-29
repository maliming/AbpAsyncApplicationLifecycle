# AbpAsyncApplicationLifecycle

### Introduction

The principle of this library is to add a new `HostedService`. And execute async methods(StartAsync/StopAsync).

Only need to implement the `IAbpAsyncApplicationLifecycle` interface.  If the Abp module implements the interface, the async method will be executed in the order of dependency of the module.

```cs
public interface IAbpAsyncApplicationLifecycle
{
      Task StartAsync(AbpAsyncApplicationLifecycleContext context);

      Task StopAsync(AbpAsyncApplicationLifecycleContext context);
}
```

```cs
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
```

### How to integrate

For web app
```cs
internal static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
            .AddAbpAsyncApplicationLifecycle<WebAppWebModule>()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                  webBuilder.UseStartup<Startup>();
            })
            .UseAbpAsyncApplicationLifecycle()
            .UseAutofac()
            .UseSerilog();
```

Web app start and stop output:
```cs
[21:27:17 INF] Starting web host.
[21:27:17 INF] User profile is available. Using 'C:\Users\maliming\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
[21:27:17 INF] Loaded ABP modules:
[21:27:17 INF] - Volo.Abp.Castle.AbpCastleCoreModule
[21:27:17 INF] - Volo.Abp.Autofac.AbpAutofacModule
[21:27:17 INF] - Volo.Abp.Localization.AbpLocalizationAbstractionsModule
[21:27:17 INF] - Volo.Abp.Validation.AbpValidationAbstractionsModule
[21:27:17 INF] - Volo.Abp.ObjectExtending.AbpObjectExtendingModule
[21:27:17 INF] - Volo.Abp.Uow.AbpUnitOfWorkModule
[21:27:17 INF] - Volo.Abp.Data.AbpDataModule
[21:27:17 INF] - Volo.Abp.VirtualFileSystem.AbpVirtualFileSystemModule
[21:27:17 INF] - Volo.Abp.Security.AbpSecurityModule
[21:27:17 INF] - Volo.Abp.MultiTenancy.AbpMultiTenancyModule
[21:27:17 INF] - Volo.Abp.Settings.AbpSettingsModule
[21:27:17 INF] - Volo.Abp.Timing.AbpTimingModule
[21:27:17 INF] - Volo.Abp.Json.AbpJsonModule
[21:27:17 INF] - Volo.Abp.Threading.AbpThreadingModule
[21:27:17 INF] - Volo.Abp.Auditing.AbpAuditingModule
[21:27:17 INF] - Volo.Abp.Http.AbpHttpAbstractionsModule
[21:27:17 INF] - Volo.Abp.Minify.AbpMinifyModule
[21:27:17 INF] - Volo.Abp.Http.AbpHttpModule
[21:27:17 INF] - Volo.Abp.Authorization.AbpAuthorizationModule
[21:27:17 INF] - Volo.Abp.Localization.AbpLocalizationModule
[21:27:17 INF] - Volo.Abp.Validation.AbpValidationModule
[21:27:17 INF] - Volo.Abp.ExceptionHandling.AbpExceptionHandlingModule
[21:27:17 INF] - Volo.Abp.AspNetCore.AbpAspNetCoreModule
[21:27:17 INF] - Volo.Abp.ApiVersioning.AbpApiVersioningAbstractionsModule
[21:27:17 INF] - Volo.Abp.EventBus.AbpEventBusModule
[21:27:17 INF] - Volo.Abp.Guids.AbpGuidsModule
[21:27:17 INF] - Volo.Abp.ObjectMapping.AbpObjectMappingModule
[21:27:17 INF] - Volo.Abp.Domain.AbpDddDomainModule
[21:27:17 INF] - Volo.Abp.Application.AbpDddApplicationContractsModule
[21:27:17 INF] - Volo.Abp.Features.AbpFeaturesModule
[21:27:17 INF] - Volo.Abp.Application.AbpDddApplicationModule
[21:27:17 INF] - Volo.Abp.AspNetCore.Mvc.AbpAspNetCoreMvcContractsModule
[21:27:17 INF] - Volo.Abp.UI.AbpUiModule
[21:27:17 INF] - Volo.Abp.GlobalFeatures.AbpGlobalFeaturesModule
[21:27:17 INF] - Volo.Abp.AspNetCore.Mvc.AbpAspNetCoreMvcModule
[21:27:17 INF] - Volo.Abp.UI.Navigation.AbpUiNavigationModule
[21:27:17 INF] - Volo.Abp.AspNetCore.Mvc.UI.AbpAspNetCoreMvcUiModule
[21:27:17 INF] - SharedModule.MyModule
[21:27:17 INF] - WebApp.Web.WebAppWebModule
[21:27:18 INF] Initialized all ABP modules.
[21:27:18 INF] Now listening on: https://localhost:5000

[21:27:18 ERR] MyModule.StartAsync
[21:27:18 ERR] WebAppWebModule.StartAsync
[21:27:18 ERR] MyService.StartAsync

[21:27:18 INF] Application started. Press Ctrl+C to shut down.
[21:27:18 INF] Hosting environment: Development
[21:27:18 INF] Content root path: WebApp
[21:27:22 INF] Application is shutting down...

[21:27:22 ERR] MyModule.StopAsync
[21:27:22 ERR] WebAppWebModule.StopAsync
[21:27:22 ERR] MyService.StopAsync

Process finished with exit code 0.
```

For console app
```cs
internal static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
            .UseAutofac()
            .UseSerilog()
            .AddAbpAsyncApplicationLifecycle<ConsoleAppModule>()
            .ConfigureServices((hostContext, services) =>
            {
                  services.AddApplication<ConsoleAppModule>();
            })
            .UseAbpAsyncApplicationLifecycle();
```

Console app start and stop output:
```cs
[21:28:32 INF] Starting console host.
[21:28:32 INF] Loaded ABP modules:
[21:28:32 INF] - Volo.Abp.Castle.AbpCastleCoreModule
[21:28:32 INF] - Volo.Abp.Autofac.AbpAutofacModule
[21:28:32 INF] - SharedModule.MyModule
[21:28:32 INF] - ConsoleApp.ConsoleAppModule
[21:28:32 INF] Initialized all ABP modules.
[21:28:32 INF] ConsoleAppHostedService.StartAsync

[21:28:32 ERR] MyModule.StartAsync
[21:28:32 ERR] ConsoleAppModule.StartAsync
[21:28:32 ERR] MyService.StartAsync

[21:28:32 INF] Application started. Press Ctrl+C to shut down.
[21:28:32 INF] Hosting environment: Production
[21:28:32 INF] Content root path: netcoreapp3.1
[21:28:34 INF] Application is shutting down...

[21:28:34 ERR] MyModule.StopAsync
[21:28:34 ERR] ConsoleAppModule.StopAsync
[21:28:34 ERR] MyService.StopAsync
[21:28:34 INF] ConsoleAppHostedService.StopAsync

Process finished with exit code 0.
```
