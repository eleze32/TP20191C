using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using Entidades;
using Entidades.ModelView;

namespace TP20191C.Controllers
{
    [Authorize]
    public class ProfesorController : Controller
    {
        // GET: Profesor

        static PreguntaViewModel pvm = new PreguntaViewModel();

        //Profesor Crea Repuesta
        public ActionResult CrearPregunta()
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Alumno")
                return RedirectToAction("Error", "Home", new { @error = 404 });

            pvm.Clase = ClaseServicio.ObtenerTodasLasClases();
            pvm.Tema = TemaServicio.ObtenerTodosLosTemas();
            pvm.Pregunta = new Pregunta
            {
                Nro = PreguntasServicio.ObtenerNumeroProximaPregunta()
            };

            return View(pvm);
        }

        [HttpPost]
        public ActionResult CrearPregunta(Pregunta pregunta)
        {
            if (!ModelState.IsValid)
            {
                pvm.Pregunta = pregunta;
                return View(pvm);
            }
            
            if(ABMPreguntasServicio.Crear(pregunta))
                return RedirectToAction("AdministrarPreguntas", "Profesor");
            
            pvm.Pregunta = pregunta;
            ViewBag.ExistePregunta = true;
            return View(pvm);
        }
        //Profesor ve todas las preguntas hechas por él
        public ActionResult AdministrarPreguntas()
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Alumno")
                return RedirectToAction("Error", "Home", new { @error = 404 });

            List<Pregunta> preguntas = PreguntasServicio.ObtenerTodasLasPreguntas();
            return View(preguntas);
        }

        //Profesor modifica una pregunta hecha por él
        public ActionResult ModificarPregunta(int id)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Alumno")
                return RedirectToAction("Error", "Home", new { @error = 404 });

            pvm.Pregunta = new Pregunta();
            pvm.Pregunta = PreguntasServicio.ObtenerPreguntaporId(id);
            pvm.Clase = ClaseServicio.ObtenerTodasLasClases();
            pvm.Tema = TemaServicio.ObtenerTodosLosTemas();
            ViewBag.HayRepuestas = pvm.Pregunta.RespuestaAlumno.Count > 0 ? true : false;

            return View(pvm);
        }
        [HttpPost]
        public ActionResult ModificarPregunta(Pregunta pregunta)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if (!ModelState.IsValid)
            {
                pvm.Pregunta = pregunta;
                ViewBag.HayRepuestas = pvm.Pregunta.RespuestaAlumno.Count > 0 ? true : false;
                return View(pvm);
            }

            if(ABMPreguntasServicio.Modificar(pregunta))
                return RedirectToAction("AdministrarPreguntas", "Profesor");

            pvm.Pregunta = pregunta;
            ViewBag.ExistePregunta = true;
            ViewBag.HayRepuestas = pvm.Pregunta.RespuestaAlumno.Count > 0 ? true : false;
            return View(pvm);
        }
        //profesor elimina una prgunta hecha por él
        public int EliminarPregunta(int id)
        {
            int eliminar = ABMPreguntasServicio.Eliminar(id);
            return eliminar;
        }

        // profesor evalaua las repuestas, listados de repuestas de alumnos
        public ActionResult EvaluarRespuestas(int id, int filtro = -1)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Alumno")
                return RedirectToAction("Error", "Home", new { @error = 404 });

            EvaluarRepuestaViewModel respuestas = RespuestasServicio.ObtenerRespuestaPorFiltro(id,filtro);
            
            return View(respuestas);
        }

        [ActionName("acerca-de")]
        public ActionResult AcercaDe()
        { 
            return RedirectToAction("acerca-de","Alumnos",new { layout= "~/Views/Shared/ProfesoresLayout.cshtml" });
        }
    }
}