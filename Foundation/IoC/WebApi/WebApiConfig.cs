using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Foundation.IoC.WebApi
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IUnityContainer container)
        {
            config.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterInstance<IHttpControllerActivator>(new UnityHttpControllerActivator(container));
            foreach (var type in
                Assembly.GetExecutingAssembly().GetExportedTypes().
                            Where(x => x.GetInterface(typeof(IHttpController).Name) != null))
            {
                container.RegisterType(type);
            }
        }
    }
}