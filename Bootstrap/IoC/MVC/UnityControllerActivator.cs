using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bootstrap.IoC.MVC
{
    public class UnityControllerActivator : IControllerActivator
    {

        private Guid _containerGuid;
        public UnityControllerActivator(Guid containerGuid)
        {
            _containerGuid = containerGuid;
        }
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            var container = requestContext.HttpContext.Items[_containerGuid] as IUnityContainer;
            if (container != null)
            {
                return container.Resolve(controllerType) as IController;
            }
            return null;
        }
    }
}