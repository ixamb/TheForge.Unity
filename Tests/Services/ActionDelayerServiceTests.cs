using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TheForge.Services.Delayer;
using TheForge.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Services
{
    public class ActionDelayerServiceTests : SingletonServiceTestBase<ActionDelayerService, IActionDelayerService>
    {
        private static readonly float[] FloatDelays = { 0.5f, 1f, 2f };
        private const float Tolerance = 0.05f;
        
        [UnityTest]
        public IEnumerator CreateDelayer_ShouldSuccess([ValueSource(nameof(FloatDelays))] float delay)
        {
            var delayCode = $"TestDelay_{delay}_{StringUtils.Random(8)}";
            Service.Delay(delay, () => {}, delayCode);
            Assert.IsNotNull(Service.Get(delayCode), $"Delayer '{delayCode}' should be registered");
            yield return null;
        }

        [UnityTest]
        public IEnumerator WaitForDelayerExecution_ShouldSuccess([ValueSource(nameof(FloatDelays))] float delay)
        {
            var startTime = Time.realtimeSinceStartup;
            var endTime = 0f;
            var executed = false;
            var delayCode = $"TestDelay_{delay}_{StringUtils.Random(8)}";
            
            Service.Delay(delay, () =>
            {
                endTime = Time.realtimeSinceStartup - startTime;
                executed = true;
            }, delayCode);
            
            Assume.That(Service.Get(delayCode), Is.Not.Null, "Delayer should be registered");
            
            yield return new WaitForSecondsRealtime(delay + 0.1f);
            Assert.IsTrue(executed, $"Action should execute after {delay}s");
            Assert.That(endTime, Is.EqualTo(delay).Within(Tolerance), $"Expected ~{delay}s, actual {endTime:F3}s");
        }

        [UnityTest]
        public IEnumerator CancelDelayerExecution_ShouldSuccess([ValueSource(nameof(FloatDelays))] float delay)
        {
            var delayCode = $"TestDelay_{delay}_{StringUtils.Random(8)}";
            Service.Delay(delay, () => { }, delayCode);
            
            Assume.That(Service.Get(delayCode), Is.Not.Null, "Delayer should be registered");
            
            yield return new WaitForSecondsRealtime(delay/2);
            Assume.That(Service.Get(delayCode), Is.Not.Null, $"Delayer with {delay}s delay should execute after {delay}s");
            Service.Cancel(delayCode);
            yield return null;
            Assert.IsNull(Service.Get(delayCode), "Delayer should be null");
        }

        [UnityTest]
        [TestCaseSource(nameof(GenerateDelaySetData))]
        public IEnumerator CancelAllDelayerExecution_ShouldSuccess(float[] delays)
        {
            var delayCodes = new List<string>();
            foreach (var delay in delays)
            {
                var delayCode = $"TestDelay_{delay}_{StringUtils.Random(8)}";
                delayCodes.Add(delayCode);
                Service.Delay(delay, () => { }, delayCode);
                Assume.That(Service.Get(delayCode), Is.Not.Null, "Delayer should be registered");
            }

            yield return null;
            Service.CancelAll();
            yield return null;
            foreach (var delayCode in delayCodes)
            {
                Assert.IsNull(Service.Get(delayCode), "Delayer should be null");
            }
        }
        
        #region delay set generators

        private static IEnumerable<TestCaseData> GenerateDelaySetData()
        {
            yield return new TestCaseData(FloatDelays).SetName("Delays_Set");
        }
        
        #endregion delay set generators
    }
}