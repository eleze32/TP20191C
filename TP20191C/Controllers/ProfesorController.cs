using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;

namespace TP20191C.Controllers
{
    [Authorize]
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
            ViewBag.Preguntas = PreguntasServicio.ObtenerPreguntas();
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

        // profesor evalua las repuestas, listados de repuestas de alumnos
        public ActionResult EvaluarRespuesta()
        {
            int idPregunta = Convert.ToInt32(Request.QueryString["idPregunta"]);
            int filtro = Convert.ToInt32(Request.QueryString["filtro"]);

            //filtro = -1 todas, 0 sin corregir, 1 correcta, 2 regular, 3 mal
            switch (filtro)
            {
                case 0:
                    ViewBag.Respuestas = RespuestasServicio.ObtenerRespuestasSinCorregirAPregunta(idPregunta);
                    break;
                case 1:
                case 2:
                case 3:
                    ViewBag.Respuestas = RespuestasServicio.ObtenerRespuestasCorregidasDePregunta(idPregunta, filtro);
                    break;
                default:
                    ViewBag.Respuestas = RespuestasServicio.ObtenerRespuestasAPregunta(idPregunta);
                    break;
            }
            ViewBag.Pregunta = PreguntasServicio.ObtenerInformacionDePregunta(idPregunta);
            ViewBag.TodasCorregidas = RespuestasServicio.VerificarSiTodasLasRespuestasEstanCorregidas(idPregunta);
            ViewBag.MejorRespuesta = RespuestasServicio.VerificarSiExisteMejorRespuesta(idPregunta);
            return View();
        }
    }
}