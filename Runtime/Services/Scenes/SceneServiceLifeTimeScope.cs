using VContainer;
using VContainer.Unity;

namespace TheForge.Services.Scenes
{
    public class SceneServiceLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
        }
    }
}