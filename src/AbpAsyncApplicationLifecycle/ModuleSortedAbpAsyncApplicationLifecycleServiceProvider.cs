using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Modularity;

namespace AbpAsyncApplicationLifecycle
{
    public class ModuleSortedAbpAsyncApplicationLifecycleServiceProvider : IAbpAsyncApplicationLifecycleServiceProvider
    {
        private readonly AbpAsyncApplicationLifecycleOptions _options;

        public ModuleSortedAbpAsyncApplicationLifecycleServiceProvider(
            IOptions<AbpAsyncApplicationLifecycleOptions> options)
        {
            _options = options.Value;
        }

        public IEnumerable<IAbpAsyncApplicationLifecycle> GetServices(IServiceProvider serviceProvider)
        {
            var abpAsyncInitializeServices = serviceProvider.GetServices<IAbpAsyncApplicationLifecycle>().ToList();
            if (abpAsyncInitializeServices.Any())
            {
                var abpModuleServices = abpAsyncInitializeServices
                    .Where(x => x.GetType().IsSubclassOf(typeof(AbpModule)))
                    .Select(x => x.As<AbpModule>());

                var modules = abpModuleServices
                    .Select(module => new AbpModuleDescriptor(module.GetType(), module, false))
                    .Cast<IAbpModuleDescriptor>().ToList();

                if (modules.Any())
                {
                    var sortedModules = modules.SortByDependencies(m => m.Dependencies);
                    sortedModules.MoveItem(x => x.Type == _options.StartupModule, modules.Count - 1);

                    return sortedModules.Select(x => x.Instance.As<IAbpAsyncApplicationLifecycle>())
                        .Concat(abpAsyncInitializeServices.Where(x => !x.GetType().IsSubclassOf(typeof(AbpModule))));
                }
            }

            return abpAsyncInitializeServices;
        }
    }
}
