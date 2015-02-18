using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Foundation.IoC.WebApi
{
    public class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver
    {
        public UnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }
        public IDependencyScope BeginScope()
        {
            return new UnityDependencyScope(Container.CreateChildContainer());
        }
    }
}