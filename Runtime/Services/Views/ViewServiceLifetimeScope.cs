using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TheForge.Services.Views
{
    public sealed class ViewServiceLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IViewService, ViewService>(Lifetime.Singleton);
            foreach (var view in FindObjectsByType<View>(FindObjectsSortMode.None))
                autoInjectGameObjects.Add(view.gameObject);
        }
    }
}