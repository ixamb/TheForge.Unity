using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace TheForge.Services.Delayer
{
    /// <summary>
    /// A service that facilitates triggering events with a delay.
    /// For each requested delay, a GameObject is created, on which an Update tracks the event triggering. The Object is then destroyed.
    /// </summary>
    public sealed class DelayerService : IDelayerService
    {
        private readonly IObjectResolver _container;
        private readonly Dictionary<string, Delayer> _actionDelayers = new();

        public DelayerService(IObjectResolver container)
        {
            _container = container;
        }
        
        /// <summary>
        /// Retrieves the delayer Object based on the code used to create it.
        /// </summary>
        [CanBeNull]
        Delayer IDelayerService.Get(string code)
        {
            return _actionDelayers.GetValueOrDefault(code);
        }
        
        /// <summary>
        /// Creates a delayer. A code can be specified to retrieve information about the delayer Object or act on it.
        /// </summary>
        void IDelayerService.Delay(float durationInSeconds, Action action, string code = "")
        {
            var delayerObject = new GameObject($"ActionDelayer_{(code == string.Empty ? Guid.NewGuid() : code)}");
            var delayer = delayerObject.AddComponent<Delayer>();
            _container.Inject(delayer);
            
            delayer.Initialize(durationInSeconds, action, () => _actionDelayers.Remove(code));
            _actionDelayers[code] = delayer;
        }

        /// <summary>
        /// Cancels a delay function based on the provided code.
        /// </summary>
        void IDelayerService.Cancel(string code)
        {
            if (_actionDelayers.Remove(code, out var delayer))
            {
                delayer?.Cancel();
            }
        }

        /// <summary>
        /// Cancels all required delay functions.
        /// </summary>
        void IDelayerService.CancelAll()
        {
            foreach (var actionDelayer in _actionDelayers.Values)
            {
                actionDelayer.Cancel();
            }
            _actionDelayers.Clear();
        }
    }
}