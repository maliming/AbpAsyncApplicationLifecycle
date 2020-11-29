using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace AbpAsyncApplicationLifecycle
{
    public class AbpAsyncApplicationLifecycleContext
    {
        public IServiceProvider ServiceProvider { get; }

        public AbpAsyncApplicationLifecycleContext([NotNull] IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            ServiceProvider = serviceProvider;
        }
    }
}
