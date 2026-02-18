using System;

namespace TheForge.Services.Scheduler
{
    /// <inheritdoc cref="ActionSchedulerService"/>
    public interface IActionSchedulerService : ISingleton
    {
        /// <inheritdoc cref="ActionSchedulerService.CreateScheduler"/>
        ActionScheduler CreateScheduler(string code, Action action, float durationInSeconds, SchedulerEndAction endAction);
        
        /// <inheritdoc cref="ActionSchedulerService.GetScheduler"/>
        ActionScheduler GetScheduler(string code);
        
        /// <inheritdoc cref="ActionSchedulerService.DestroyScheduler"/>
        void DestroyScheduler(string code);
        
        /// <inheritdoc cref="ActionSchedulerService.DestroyAllSchedulers"/>
        void DestroyAllSchedulers();
    }
}