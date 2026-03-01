using VContainer;
using VContainer.Unity;

namespace TheForge.Services.Delayer
{
    public class DelayerServiceLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDelayerService, DelayerService>(Lifetime.Singleton);
        }
    }
}