using System;

namespace TheForge.Services.Delayer
{
    /// <inheritdoc cref="DelayerService"/>
    public interface IDelayerService
    {
        /// <inheritdoc cref="DelayerService.Get"/>
        Delayer Get(string code);
        
        /// <inheritdoc cref="DelayerService.Delay"/>
        void Delay(float durationInSeconds, Action action, string code = "");
        
        /// <inheritdoc cref="DelayerService.Cancel"/>
        void Cancel(string code);
        
        /// <inheritdoc cref="DelayerService.CancelAll"/>
        void CancelAll();
    }
}