using VContainer;
using VContainer.Unity;

namespace TheForge.Services.Scheduler
{
    public sealed class SchedulerServiceLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISchedulerService, SchedulerService>(Lifetime.Singleton);
        }
    }
}