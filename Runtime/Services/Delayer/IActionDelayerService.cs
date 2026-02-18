using System;

namespace TheForge.Services.Delayer
{
    /// <inheritdoc cref="ActionDelayerService"/>
    public interface IActionDelayerService : ISingleton
    {
        /// <inheritdoc cref="ActionDelayerService.Get"/>
        ActionDelayer Get(string code);
        
        /// <inheritdoc cref="ActionDelayerService.Delay"/>
        void Delay(float durationInSeconds, Action action, string code = "");
        
        /// <inheritdoc cref="ActionDelayerService.Cancel"/>
        void Cancel(string code);
        
        /// <inheritdoc cref="ActionDelayerService.CancelAll"/>
        void CancelAll();
    }
}