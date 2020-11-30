using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.DependencyInjection;

namespace AbpAsyncApplicationLifecycle
{
    public class AbpAsyncApplicationLifecycleConventionalRegistrar : DefaultConventionalRegistrar
    {
        public override void AddType(IServiceCollection services, Type type)
        {
            if (typeof(IAbpAsyncApplicationLifecycle).IsAssignableFrom(type))
            {
                services.TryAddEnumerable(new ServiceDescriptor(typeof(IAbpAsyncApplicationLifecycle),
                    type,
                    GetLifeTimeOrNull(type, GetDependencyAttributeOrNull(type)) ?? ServiceLifetime.Transient));
            }
        }
    }
}
