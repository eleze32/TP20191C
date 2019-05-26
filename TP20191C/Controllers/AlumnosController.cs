using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP20191C.Controllers
{
    public class AlumnosController : Controller
    {
        // alumno ve su repuesta
        public ActionResult VerRepuesta()
        {
            return View();
        }

        // Alumnos ve todas las preguntas, ya sea que no respondio, si respondio, y sus calificaiones
        public ActionResult VerPreguntas()
        {
            return View();
        }

        // Alumno responde pregunta
        public ActionResult ResponderPregunta()
        {
            return View();
        }

        public ActionResult AcercaDe()
        {
            return View();
        }
    }
}