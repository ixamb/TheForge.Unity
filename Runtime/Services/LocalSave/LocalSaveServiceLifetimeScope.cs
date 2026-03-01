using VContainer;
using VContainer.Unity;

namespace TheForge.Services.LocalSave
{
    public sealed class LocalSaveServiceLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ILocalSaveService, LocalSaveService>(Lifetime.Singleton);
        }
    }
}