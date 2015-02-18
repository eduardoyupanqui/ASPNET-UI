using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Foundation.IoC.WebApi
{
    public class UnityDependencyScope : IDependencyScope
    {
        private IUnityContainer _container;
        protected IUnityContainer Container { get { return _container; } }
        public UnityDependencyScope(IUnityContainer container)
        {
            _container = container;
        }
        public void Dispose()
        {
            _container.Dispose();
        }
        public object GetService(Type serviceType)
        {
            return _container.IsRegistered(serviceType)
                        ? _container.Resolve(serviceType)
                        : null;
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }
    }
}