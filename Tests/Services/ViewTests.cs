using System.Collections;
using NUnit.Framework;
using TheForge.Services.Views;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Services
{
    public sealed class ViewTests
    {
        private const string TestViewCode = "Test_View";
        private MockView _mockView;
        
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            if (_mockView is not null)
            {
                Object.Destroy(_mockView.gameObject);
            }
            _mockView = new GameObject(TestViewCode).AddComponent<MockView>();
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (_mockView is not null)
            {
                Object.Destroy(_mockView.gameObject);
                _mockView = null;
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShowViewWithoutAnimation_ShouldSuccess()
        {
            _mockView.ShowView();
            var canvasGroup = _mockView.GetComponent<CanvasGroup>();
            Assume.That(canvasGroup, Is.Not.Null);
            
            Assert.AreEqual(1, canvasGroup.alpha);
            Assert.AreEqual(true, canvasGroup.blocksRaycasts);
            Assert.AreEqual(true, canvasGroup.interactable);
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator HideViewWithoutAnimation_ShouldSuccess()
        {
            _mockView.HideView();
            var canvasGroup = _mockView.GetComponent<CanvasGroup>();
            Assume.That(canvasGroup, Is.Not.Null);
            
            Assert.AreEqual(0, canvasGroup.alpha);
            Assert.AreEqual(false, canvasGroup.blocksRaycasts);
            Assert.AreEqual(false, canvasGroup.interactable);
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator HideViewWithoutAnimation_ShouldFail()
        {
            _mockView.ShowView();
            Assume.That(_mockView.IsVisibleAndActive(), Is.True);
            _mockView._TestAlterKeepViewDisplayedProperty(true);
            _mockView.HideView();
            Assert.That(_mockView.IsVisibleAndActive(), Is.True);
            yield return null;
        }

        private sealed class MockView : View
        {
            internal void _TestAlterUseAnimationProperty(bool newValue) => useAnimation = newValue;
            internal void _TestAlterKeepViewDisplayedProperty(bool newValue) => keepViewDisplayed = newValue;
        }
    }
}