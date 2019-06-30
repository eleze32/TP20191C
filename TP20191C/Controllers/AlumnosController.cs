using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP20191C.Controllers
{   
    [Authorize]
    public class AlumnosController : Controller
    {
        // alumno ve su repuesta
        public ActionResult VerRespuesta(int id)
        {
            //Seguridad: Verifico que la Pregunta tenga fecha de respuesta, en caso contrario redirijo a Preguntas
            if (PreguntasServicio.verFechaPreguntaValida(id))
            {
                return RedirectToAction("VerPreguntas", "Alumnos");
            }
            //Verifico si se vencio el plazo de la pregunta
            ViewBag.plazo = PreguntasServicio.plazoFecha(id);

            RespuestaAlumno ra = PreguntasServicio.verRespuesta((int)Session["UsuarioId"], id);
            return View(ra);
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