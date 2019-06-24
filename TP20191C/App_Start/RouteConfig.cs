using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TP20191C
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Ingresar",
                defaults: new
                {
                    controller = "Home",
                    action = "Ingresar",
                    email = UrlParameter.Optional,
                    password = UrlParameter.Optional,
                    soyProfesor = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Inicio", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VerRespuesta",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Alumnos", action = "VerRespuesta", id = UrlParameter.Optional }
            );
        }
    }
}
