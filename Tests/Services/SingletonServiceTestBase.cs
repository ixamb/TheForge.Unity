using NUnit.Framework;
using VContainer;

namespace Tests.Services
{
    // ReSharper disable once InconsistentNaming
    public abstract class SingletonServiceTestBase<ITService, TService> where TService : ITService
    {
        protected ITService Service;
        private IObjectResolver _container;
        
        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.Register<ITService, TService>(Lifetime.Singleton);
            _container = builder.Build();
            Service = _container.Resolve<ITService>();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }
    }
}