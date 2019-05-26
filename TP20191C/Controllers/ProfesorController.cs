using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP20191C.Controllers
{
    public class ProfesorController : Controller
    {
        // GET: Profesor

        //Profesor Crea Repuesta
        public ActionResult CrearPregunta()
        {
            return View();
        }

        //Profesor ve todas las preguntas hechas por él
        public ActionResult AdministrarPreguntas()
        {
            return View();
        }

        //Profesor modifica una pregunta hecha por él
        public ActionResult ModificarPregunta()
        {
            return View();
        }

        //profesor elimina una prgunta hecha por él
        public ActionResult EliminarPregunta()
        {
            return View();
        }

        // profesor evalaua las repuestas, listados de repuestas de alumnos
        public ActionResult EvaluarRepuestas()
        {
            return View();
        }
    }
}