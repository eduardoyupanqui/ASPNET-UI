using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Foundation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            //navega para: juegos/xbox-360
            routes.MapRoute(
                name: "Consolas",
                url: "juegos/{nombreconsola}",
                defaults: new { controller = "Juegos", action = "Index", nombreconsola = UrlParameter.Optional });

            //navega para: juegos/xbox-360/halo-4
            routes.MapRoute(
                name: "Juegos",
                url: "Juegos/{nombreconsola}/{nombreJuego}",
                defaults: new { controller = "Juegos", action="VerJuego"});

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}