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
            if (PreguntasServicio.verFechaPreguntaValida(id)) {
                return RedirectToAction("VerPreguntas", "Alumnos");
            }
            //Verifico si se vencio el plazo de la pregunta
            ViewBag.plazo = PreguntasServicio.plazoFecha(id);

            RespuestaAlumno ra = PreguntasServicio.verRespuesta((int)Session["UsuarioId"],id);
            return View(ra);
        }

        // Alumnos ve todas las preguntas, ya sea que no respondio, si respondio, y sus calificaiones
        public ActionResult VerPreguntas()
        {
            ViewBag.Todas = PreguntasServicio.preguntasTodas((int)Session["UsuarioId"]);
            ViewBag.SinCorregir = PreguntasServicio.preguntasSinCorregir((int)Session["UsuarioId"]);
            ViewBag.Regular = PreguntasServicio.preguntasRegular((int)Session["UsuarioId"]);
            ViewBag.Correctas = PreguntasServicio.preguntasCorrectas((int)Session["UsuarioId"]);
            ViewBag.Mal = PreguntasServicio.preguntasMal((int)Session["UsuarioId"]);
            return View();
        }

        // Alumno responde pregunta
        public ActionResult ResponderPregunta(int id)
        {
            //Seguridad: Verifico que la Pregunta no haya sido respondida, en caso contrario redirijo a VerRespuesta
            if (PreguntasServicio.verPreguntaValidaAlumno((int)Session["UsuarioId"],id))
            {
                return RedirectToAction("VerRespuesta", "Alumnos",id);
            }

            ViewBag.Pregunta = PreguntasServicio.obtenerPregunta(id);
            ViewBag.Fecha = PreguntasServicio.plazoFecha(id);
            //Guardo si la fecha de la pregunta vencio su plazo
            /*bool Fecha;

            if (ViewBag.Pregunta.FechaDisponibleHasta == null)
            {
                Fecha = true;
                ViewBag.Fecha = Fecha; }
            else
            {
                int fecha = DateTime.Compare(ViewBag.Pregunta.FechaDisponibleHasta, DateTime.Now);
                if (fecha <= 0)
                {
                    Fecha = true;
                    ViewBag.Fecha = Fecha;
                }
                else
                {
                    Fecha = false;
                    ViewBag.Fecha = Fecha;
                }
            }*/

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ResponderPregunta(RespuestaAlumno ra) {
            PreguntasServicio.guardarRespuesta(ra, (int)Session["UsuarioId"]);
            
            return RedirectToAction("VerPreguntas", "Alumnos");
        }



        public ActionResult AcercaDe()
        {
            return View();
        }
    }
}