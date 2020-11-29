using System;
using System.Collections.Generic;

namespace AbpAsyncApplicationLifecycle
{
    public interface IAbpAsyncApplicationLifecycleServiceProvider
    {
        IEnumerable<IAbpAsyncApplicationLifecycle> GetServices(IServiceProvider serviceProvider);
    }
}
