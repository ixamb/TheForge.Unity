using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace TheForge.Services.Scheduler
{
    /// <summary>
    /// A service that simplifies scheduling to trigger recurring events.
    /// When a scheduling request is made, a GameObject is created.
    /// The scheduling delay is handled by a separate local Update, which can then trigger the event according to the specified parameters.
    /// </summary>
    public sealed class ActionSchedulerService : Singleton<ActionSchedulerService, IActionSchedulerService>, IActionSchedulerService
    {
        private readonly Dictionary<string, ActionScheduler> _actionSchedulers = new();

        protected override void Init()
        {
        }

        /// <summary>
        /// Creates a scheduler, that will trigger an action after an elapse time specified in parameter.<br />
        /// It provides the possibility to choose an end-delay action (repeat the action, pause for further usage, destroy it for one-shot usage)
        /// </summary>
        public ActionScheduler CreateScheduler(string code, Action action, float durationInSeconds, SchedulerEndAction endAction)
        {
            if (_actionSchedulers.ContainsKey(code))
            {
                _actionSchedulers[code].Initialize(action, durationInSeconds, endAction, OnSchedulerDestroyed);
                _actionSchedulers[code].Restart();
                return _actionSchedulers[code];
            }
            
            var actionScheduler = Instantiate(new GameObject($"ActionScheduler_{code}").AddComponent<ActionScheduler>(), gameObject.transform);
            actionScheduler.Initialize(action, durationInSeconds, endAction, OnSchedulerDestroyed);
            _actionSchedulers.Add(code, actionScheduler);
            return actionScheduler;

            void OnSchedulerDestroyed() => _actionSchedulers.Remove(code);
        }

        /// <summary>
        /// Retrieves a scheduler depending on the code specified during its initialization.
        /// </summary>
        [CanBeNull]
        public ActionScheduler GetScheduler(string code)
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
                Destroy(actionScheduler.gameObject);
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
                Destroy(actionScheduler.gameObject);
            }
            _actionSchedulers.Clear();
        }
    }
}