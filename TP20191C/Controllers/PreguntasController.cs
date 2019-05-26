using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP20191C.Controllers
{
    public class PreguntasController : Controller
    {
        // GET: Preguntas
        public ActionResult Index()
        {
            return View();
        }

        //Profesor Crea Repuesta
        public ActionResult Crear()
        {
            return View();
        }

        //Profesor ve todas las preguntas hechas por él
        public ActionResult Administrar()
        {
            return View();
        }

        //Profesor modifica una pregunta hecha por él
        public ActionResult Modificar()
        {
            return View();
        }

        //profesor elimina una prgunta hecha por él
        public ActionResult Eliminar()
        {
            return View();
        }

        // Alumnos ve todas las preguntas, ya sea que no respondio, si respondio, y sus calificaiones
        public ActionResult VerPreguntasAlumnos()
        {
            return View();
        }

        // Alumno responde pregunta
        public ActionResult Responder()
        {
            return View();
        }
    }
}