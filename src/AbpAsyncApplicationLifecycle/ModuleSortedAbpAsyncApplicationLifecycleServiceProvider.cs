using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpAsyncApplicationLifecycle
{
    public class ModuleSortedAbpAsyncApplicationLifecycleServiceProvider : IAbpAsyncApplicationLifecycleServiceProvider
    {
        public IEnumerable<IAbpAsyncApplicationLifecycle> GetServices(IServiceProvider serviceProvider, Type startupModuleType)
        {
            var abpAsyncInitializeServices = serviceProvider.GetServices<IAbpAsyncApplicationLifecycle>().ToList();
            if (abpAsyncInitializeServices.Any())
            {
                var abpModuleDescriptors = abpAsyncInitializeServices
                    .Where(x => x is IAbpModule)
                    .Select(x => x.As<IAbpModule>())
                    .Select(module => new AbpModuleDescriptor(module.GetType(), module, false))
                    .Cast<IAbpModuleDescriptor>()
                    .ToList();

                if (abpModuleDescriptors.Any())
                {
                    var sortedModules = abpModuleDescriptors.SortByDependencies(m => m.Dependencies);
                    sortedModules.MoveItem(x => x.Type == startupModuleType, abpModuleDescriptors.Count - 1);

                    return sortedModules.Select(x => x.Instance.As<IAbpAsyncApplicationLifecycle>())
                        .Concat(abpAsyncInitializeServices.Where(x => !(x is IAbpModule)));
                }
            }

            return abpAsyncInitializeServices;
        }
    }
}
