namespace TheForge.Services.Scheduler
{
    /// <summary>
    /// Defines the action behavior triggered at the end of the defined scheduled-time on scheduler object.
    /// </summary>
    public enum SchedulerEndAction
    {
        /// <summary>
        /// At the end of the defined scheduled-time, the scheduler is destroyed. Ideal for one-shot delays.
        /// </summary>
        Destroy,
        
        /// <summary>
        /// At the end of the defined scheduled-time, the scheduler is paused. Ideal to manually relaunch it later.
        /// </summary>
        Pause,
        
        /// <summary>
        /// At the end of the defined scheduled-time, the scheduler is repeated. Ideal for tasks to be repeated regularly.
        /// </summary>
        Repeat
    }
}