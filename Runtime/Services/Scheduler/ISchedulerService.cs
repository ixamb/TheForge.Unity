using System;

namespace TheForge.Services.Scheduler
{
    /// <inheritdoc cref="SchedulerService"/>
    public interface ISchedulerService
    {
        /// <inheritdoc cref="SchedulerService.CreateScheduler"/>
        Scheduler CreateScheduler(string code, Action action, float durationInSeconds, SchedulerEndAction endAction);
        
        /// <inheritdoc cref="SchedulerService.GetScheduler"/>
        Scheduler GetScheduler(string code);
        
        /// <inheritdoc cref="SchedulerService.DestroyScheduler"/>
        void DestroyScheduler(string code);
        
        /// <inheritdoc cref="SchedulerService.DestroyAllSchedulers"/>
        void DestroyAllSchedulers();
    }
}