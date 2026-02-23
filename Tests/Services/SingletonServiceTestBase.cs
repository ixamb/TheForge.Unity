using System.Collections;
using TheForge.Services;
using TheForge.Services.Views;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Services
{
    // ReSharper disable once InconsistentNaming
    public abstract class SingletonServiceTestBase<T, IT> where T : Singleton<T, IT> where IT : class, ISingleton
    {
        protected GameObject ServiceObject;
        protected IT Service;
        
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            ResetSingletonInstance();
            yield return null;
            
            var objectName = typeof(T).Name;
            
            var existing = GameObject.Find(objectName);
            if (existing != null)
            {
                Object.DestroyImmediate(existing);
                yield return null;
            }
            
            ServiceObject = new GameObject(objectName);
            var component = ServiceObject.AddComponent<ViewService>();
            Service = component as IT;
            
            yield return AdditionalSetUp();
        }
        
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (ServiceObject != null)
            {
                Object.Destroy(ServiceObject);
                ServiceObject = null;
                Service = null;
            }
            
            yield return null;
            ResetSingletonInstance();
            yield return AdditionalTearDown();
        }
        
        private static void ResetSingletonInstance()
        {
            var resetMethod = typeof(T).GetMethod("ResetInstance", 
                System.Reflection.BindingFlags.Static | 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Public);
        
            if (resetMethod != null)
            {
                resetMethod.Invoke(null, null);
            }
        }

        protected virtual IEnumerable AdditionalSetUp()
        {
            yield return null;
        }

        protected virtual IEnumerable AdditionalTearDown()
        {
            yield return null;
        }
    }
}