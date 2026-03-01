using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace TheForge.Services.Scheduler
{
    /// <summary>
    /// A service that simplifies scheduling to trigger recurring events.
    /// When a scheduling request is made, a GameObject is created.
    /// The scheduling delay is handled by a separate local Update, which can then trigger the event according to the specified parameters.
    /// </summary>
    public sealed class SchedulerService : ISchedulerService
    {
        private readonly IObjectResolver _container;
        private readonly Dictionary<string, Scheduler> _actionSchedulers = new();
        
        public SchedulerService(IObjectResolver container) => _container = container;
        
        /// <summary>
        /// Creates a scheduler that will trigger an action after an elapsed time specified in the parameter.<br />
        /// It provides the possibility to choose an end-delay action (repeat the action, pause for further usage, destroy it for one-shot usage)
        /// </summary>
        public Scheduler CreateScheduler(string code, Action action, float durationInSeconds, SchedulerEndAction endAction)
        {
            if (_actionSchedulers.ContainsKey(code))
            {
                _actionSchedulers[code].Initialize(action, durationInSeconds, endAction, OnSchedulerDestroyed);
                _actionSchedulers[code].Restart();
                return _actionSchedulers[code];
            }
            
            var go = new GameObject($"ActionScheduler_{code}");
            var actionScheduler = go.AddComponent<Scheduler>();
            _container.Inject(actionScheduler);
            _actionSchedulers.Add(code, actionScheduler);
            return actionScheduler;

            void OnSchedulerDestroyed() => _actionSchedulers.Remove(code);
        }

        /// <summary>
        /// Retrieves a scheduler depending on the code specified during its initialization.
        /// </summary>
        [CanBeNull]
        public Scheduler GetScheduler(string code)
        {
            if (_actionSchedulers.TryGetValue(code, out var actionScheduler))
                return actionScheduler;
            
            Debug.LogWarning($"Warning: scheduler with code {code} could not be found.");
            return null;
        }

        /// <summary>
        /// Destroys a scheduler depending on the code specified during its initialization.
        /// </summary>
        public void DestroyScheduler(string code)
        {
            if (_actionSchedulers.TryGetValue(code, out var actionScheduler))
            {
                actionScheduler.Stop();
                UnityEngine.Object.Destroy(actionScheduler.gameObject);
                _actionSchedulers.Remove(code);
                return;
            }
            
            Debug.LogWarning($"Warning: scheduler with code {code} could not be found.");
        }
        
        /// <summary>
        /// Destroys all active schedulers, independently of their current state.
        /// </summary>
        public void DestroyAllSchedulers()
        {
            foreach (var actionScheduler in _actionSchedulers.Values)
            {
                actionScheduler.Stop();
                UnityEngine.Object.Destroy(actionScheduler.gameObject);
            }
            _actionSchedulers.Clear();
        }
    }
}