using System.Threading.Tasks;

namespace AbpAsyncApplicationLifecycle
{
    public interface IAbpAsyncApplicationLifecycle
    {
        Task StartAsync(AbpAsyncApplicationLifecycleContext context);

        Task StopAsync(AbpAsyncApplicationLifecycleContext context);
    }
}
