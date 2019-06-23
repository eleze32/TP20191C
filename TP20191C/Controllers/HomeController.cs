using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Entidades;
using Servicios;

namespace TP20191C.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Inicio()
        {
            if(Session["Usuario"] != null)
            {
            List<Alumno> al = AlumnoServicio.TablaPosiciones();

            ViewBag.Preguntas = PreguntasServicio.UltimasDospreguntas();



            return View(al);
            }

            return Redirect("/Ingresar");
        }

        public ActionResult Ingresar(String returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Ingresar(string email, string password, string returnUrl, bool soyProfesor = false)
        {
            bool login = UsuarioServicio.Ingresar(email, password, soyProfesor);

            if (login)
            {
                FormsAuthentication.SetAuthCookie(email, false);

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                if (soyProfesor)
                {
                    return RedirectToAction("AdministrarPreguntas", "Profesor");
                }                    
                else
                {
                    ViewBag.soyProfesor = soyProfesor;
                    return RedirectToAction("Inicio", "Home");
                }
                   
            }

            ViewBag.returnUrl = returnUrl;
            ViewBag.MensajeError = true;
            return View();
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Ingresar", "Home");
        }
    }
}