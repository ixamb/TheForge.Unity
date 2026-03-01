using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TheForge.Services.Audio
{
    public sealed class AudioServiceLifetimeScope : LifetimeScope
    {
        [SerializeField] private AudioServiceProperties audioServiceProperties;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IAudioService, AudioService>(Lifetime.Singleton).WithParameter(audioServiceProperties);
        }
    }
}