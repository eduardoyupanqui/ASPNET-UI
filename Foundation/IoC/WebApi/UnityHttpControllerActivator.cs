using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Foundation.IoC.WebApi
{
    public class UnityHttpControllerActivator : IHttpControllerActivator
    {
        private IUnityContainer _container;
        public UnityHttpControllerActivator(IUnityContainer container)
        {
            _container = container;
        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var scope = request.GetDependencyScope();
            return scope.GetService(controllerType) as IHttpController;
        }
    }
}