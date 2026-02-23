using System.Collections;
using System.Linq;
using NUnit.Framework;
using TheForge.Services.Views;
using UnityEngine.TestTools;

namespace Tests.Services
{
    public sealed class ViewServiceTests : SingletonServiceTestBase<ViewService, IViewService>
    {
        private const string DefaultMockViewCode = "Test_MockView";
        private const string FormatMockViewCode = "Test_MockView_{0}";
        private const string SpaceMockViewCode = "   ";
        
        [UnityTest]
        public IEnumerator RegisterView_ShouldSuccess()
        {
            var mockView = new MockView(DefaultMockViewCode);
            Service.RegisterView(mockView);
            Assert.IsNotNull(Service.GetView(DefaultMockViewCode), "Service.GetView(mockViewCode) != null");
            yield return null;
        }

        [UnityTest]
        public IEnumerator RegisterView_ShouldFail()
        {
            var mockView = new MockView(SpaceMockViewCode);
            Service.RegisterView(mockView);
            Assert.IsNull(Service.GetView(SpaceMockViewCode), "Service.GetView(mockViewCode) != null");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UnregisterView_ShouldSuccess()
        {
            var mockView = new MockView(DefaultMockViewCode);
            Service.RegisterView(mockView);
            Assume.That(Service.GetView(DefaultMockViewCode), Is.Not.Null);
            Service.UnregisterView(mockView);
            Assert.IsNull(Service.GetView(DefaultMockViewCode), "Service.GetView(mockViewCode) != null");
            yield return null;
        }

        [UnityTest]
        public IEnumerator GetActiveAndEnabledViews_ShouldSuccess()
        {
            var activeViewsArray = new[] {false, true, true, false};
            for (var i = 0; i < activeViewsArray.Length; i++)
            {
                var mockView = new MockView(code: string.Format(FormatMockViewCode, i), isVisibleAndActive: activeViewsArray[i]);
                Service.RegisterView(mockView);
            }

            var activeAndEnabledViews = Service.GetActiveAndEnabledViews().ToList();
            Assert.AreEqual(activeAndEnabledViews.Count, activeViewsArray.Count(t => t));
            foreach (var activeView in activeAndEnabledViews)
            {
                Assert.IsTrue(activeView.IsVisibleAndActive());
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator GetTypedView_ShouldSuccess()
        {
            var mockView = new MockView(DefaultMockViewCode);
            Service.RegisterView(mockView);
            Assume.That(Service.GetView(DefaultMockViewCode), Is.Not.Null);
            Assert.AreEqual(mockView.GetType(), Service.GetView<MockView>(DefaultMockViewCode).GetType());
            yield return null;
        }

        private class MockView : IView
        {
            private readonly string _viewCode;
            private readonly bool _isVisibleAndActive;
            
            internal MockView(string code, bool isVisibleAndActive = true)
            {
                _viewCode = code;
                _isVisibleAndActive = isVisibleAndActive;
            }
            
            public void ShowView() {}

            public void HideView() {}

            public string GetCode() => _viewCode;

            public bool IsVisibleAndActive() => _isVisibleAndActive;
        }
    }
}