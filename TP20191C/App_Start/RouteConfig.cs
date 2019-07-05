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
                name: "preguntas alumnos",
                url: "alumnos/preguntas",
                defaults: new
                {
                    controller = "Alumnos",
                    action = "VerPreguntas"
                }
            );

            routes.MapRoute(
                name: "salir alumnos",
                url: "alumnos/inicio/salir",
                defaults: new
                {
                    controller = "Home",
                    action = "Salir"
                }
            );

            routes.MapRoute(
                name: "salir profesor",
                url: "profesores/inicio/salir",
                defaults: new
                {
                    controller = "Home",
                    action = "Salir"
                }
            );

            routes.MapRoute(
                name: "Preguntas Crear",
                url: "profesores/preguntas/crear",
                defaults: new
                {
                    controller = "Profesor",
                    action = "CrearPregunta"
                }
            );

            routes.MapRoute(
                name: "Preguntas Porfesor",
                url: "profesores/preguntas",
                defaults: new
                {
                    controller = "Profesor",
                    action = "AdministrarPreguntas"
                }
            );
            routes.MapRoute(
                name: "Login",
                url: "Ingresar",
                defaults: new { controller = "Home", action = "Ingresar"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Inicio", id = UrlParameter.Optional}
            );
        }
    }
}
