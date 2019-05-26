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

           /* routes.MapRoute(
                name: "Profesor_listado_Preguntas",
                url: "profesor/Preguntas",
                defaults: new { controller = "Preguntas", action = "Administrar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Profesor",
                url: "profesor/{controller}/{action}/{id}",
                defaults: new { controller = "Preguntas", action = "Administrar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Alumno",
                url: "alumno/{controller}/{action}/{id}",
                defaults: new { controller = "Preguntas", action = "Administrar", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Inicio", id = UrlParameter.Optional }
            );
        }
    }
}
