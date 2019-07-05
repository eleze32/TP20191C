using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using Entidades;
using Entidades.ModelView;
using System.Web.Security;

namespace TP20191C.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Inicio()
        {
            if(Session["Usuario"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Profesor")
                return RedirectToAction("AdministrarPreguntas", "Profesor");
            //return RedirectToAction("Error", "Home", new { @error = 404 });

            List<Alumno> al = AlumnoServicio.TablaPosiciones();
            ViewBag.Preguntas = PreguntasServicio.UltimasDospreguntas();
            ViewBag.SinResponder = PreguntasServicio.preguntasSinResponder((int)Session["UsuarioId"]);
            return View(al);
        }

        public ActionResult Ingresar(String ReturnUrl)
        {
            ViewBag.returnUrl = ReturnUrl;
            return View();
        }
        
        [HttpPost]
        public ActionResult Ingresar(UsuarioViewModel usuario, string ReturnUrl)
        {
            bool login;
            if (!ModelState.IsValid)
            {
                ViewBag.returnUrl = ReturnUrl;
                return View(usuario);
            }
            
            login = UsuarioServicio.Ingresar(usuario);

            if (login)
            {
                FormsAuthentication.SetAuthCookie(usuario.Email, false);

                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);

                if (usuario.SoyProf)
                    return RedirectToAction("AdministrarPreguntas", "Profesor");

                return RedirectToAction("Inicio", "Home");
            }
            ViewBag.returnUrl = ReturnUrl;
            ViewBag.MensajeError = true;
            return View();
        }

        public ActionResult Salir()
        {
            Session["Usuario"] = null;
            Session["UsuarioId"] = null;
            Session["TipoUsuario"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Ingresar","Home");
        }

        public ActionResult Error(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.NroError = "505";
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.NroError = "404";
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }

            return View();

        }
    }
}