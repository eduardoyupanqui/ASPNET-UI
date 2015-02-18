using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bootstrap.App_Start;
using Microsoft.Practices.Unity;
using DataAccessLayer;
using Bootstrap.IoC.MVC;

namespace Bootstrap
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static Guid _unityGuid;
        private static IUnityContainer _container;
 
        protected void Application_Start()
        {
            _container = new UnityContainer();
            _container
                .RegisterType<IConsolas, Consolas>()
                .RegisterType<IGeneros, Generos>()
                .RegisterType<IJuegos, Juegos>()
                .RegisterType<TrailersDeVideoJuegosEntities>(new HierarchicalLifetimeManager());
            _unityGuid = Guid.NewGuid();

            MvcConfig.Register(_container, _unityGuid);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapBundleConfig.RegisterBundles();
        }

        protected void Application_BeginRequest()
        {
            if (!Request.IsWebApiRequest())
            {
                //MvcConfig.Register(_container, _unityGuid);
            }

            var childContainer = _container.CreateChildContainer();
            HttpContext.Current.Items[_unityGuid] = childContainer;
        }

        protected void Application_EndRequest()
        {
            var childContainer = HttpContext.Current.Items[_unityGuid] as IUnityContainer;
            if (childContainer != null)
            {
                childContainer.Dispose();
                HttpContext.Current.Items.Remove(_unityGuid);
            }
        }
    }
    public static class HttpRequestExtensions
    {
        public static bool IsWebApiRequest(this HttpRequest @this)
        {
            return @this.FilePath.StartsWith("/api/");
        }
    }
}