using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Entidades;
using Servicios;
using TP20191C.Models;

namespace TP20191C.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Inicio()
        {
            /*if(Session["Usuario"] != null)
            {*/
            List<Alumno> al = AlumnoServicio.TablaPosiciones();

            ViewBag.Preguntas = PreguntasServicio.ultimasDospreguntas();
            
            ViewBag.SinResponder = PreguntasServicio.preguntasSinResponder((int)Session["UsuarioId"]);

            return View(al);
            /*}*/

            /*return View();*/
        }

        public ActionResult Ingresar(String returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Ingresar(Usuario usuario, string returnUrl, bool soyProfesor = false)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            bool login = UsuarioServicio.ingresar(usuario.Email, usuario.Password, soyProfesor);

            if (login)
            {
                FormsAuthentication.SetAuthCookie(usuario.Email, false);

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                if (soyProfesor)
                    return RedirectToAction("AdministrarPreguntas", "Profesor");

                return RedirectToAction("Inicio", "Home");
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