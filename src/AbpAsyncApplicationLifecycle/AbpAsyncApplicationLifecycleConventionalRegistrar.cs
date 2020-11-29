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
                var lifeTime = GetLifeTimeOrNull(type, GetDependencyAttributeOrNull(type));
                services.TryAddEnumerable(new ServiceDescriptor(typeof(IAbpAsyncApplicationLifecycle), type, lifeTime ?? ServiceLifetime.Transient));
            }
        }
    }
}
