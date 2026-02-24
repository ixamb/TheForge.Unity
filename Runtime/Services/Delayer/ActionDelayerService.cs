using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace TheForge.Services.Delayer
{
    /// <summary>
    /// A service that facilitates triggering events with a delay.
    /// For each requested delay, a GameObject is created, on which an Update tracks the event triggering. The Object is then destroyed.
    /// </summary>
    public sealed class ActionDelayerService : Singleton<ActionDelayerService, IActionDelayerService>, IActionDelayerService
    {
        private readonly Dictionary<string, ActionDelayer> _actionDelayers = new();
        
        protected override void Init()
        {
        }

        /// <summary>
        /// Retrieves the delayer Object based on the code used to create it.
        /// </summary>
        [CanBeNull]
        public ActionDelayer Get(string code)
        {
            return _actionDelayers.GetValueOrDefault(code);
        }
        
        /// <summary>
        /// Creates a delayer. A code can be specified to retrieve information about the delayer Object, or act on it.
        /// </summary>
        public void Delay(float durationInSeconds, Action action, string code = "")
        {
            var delayerCode = $"ActionDelayer_{(code == string.Empty ? Guid.NewGuid() : code)}";
            var delayerObject = new GameObject(delayerCode);
            delayerObject.transform.SetParent(gameObject.transform);
            var actionDelayer = delayerObject.AddComponent<ActionDelayer>();
            actionDelayer.Initialize(durationInSeconds, action, () => _actionDelayers.Remove(code));
            _actionDelayers[code] = actionDelayer;
        }

        /// <summary>
        /// Cancels a delay function based on the provided code.
        /// </summary>
        public void Cancel(string code)
        {
            if (_actionDelayers.Remove(code, out var delayer))
            {
                delayer?.Cancel();
            }
        }

        /// <summary>
        /// Cancels all required delay functions.
        /// </summary>
        public void CancelAll()
        {
            foreach (var actionDelayer in _actionDelayers.Values)
            {
                actionDelayer.Cancel();
            }
            _actionDelayers.Clear();
        }
    }
}