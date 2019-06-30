using System;
using System.Web.Mvc;
using Servicios;

namespace TP20191C.Controllers
{
    public class EvaluarController : Controller
    {
        // profesor marca una respuesta como la mejor
        public ActionResult MarcarComoMejorRespuesta()
        {
            int idRespuesta = Convert.ToInt32(Request.QueryString["idRespuesta"]);
            int idPreg = Convert.ToInt32(Request.QueryString["idPregunta"]);

            EvaluadorServicio.MarcarComoMejorRespuesta(idRespuesta);

            EmailAAlumnosServicio correo = new EmailAAlumnosServicio();
            correo.GenerarEmailMejorRespuesta(idRespuesta);

            return RedirectToAction("EvaluarRespuesta", "Profesor", new { idPregunta = idPreg, filtro = -1 });            
        }

        // profesor evalua una respuesta como correcta, regular o mal
        public ActionResult EvaluarRespuesta()
        {
            int idRespuesta = Convert.ToInt32(Request.QueryString["idRespuesta"]);
            int idPreg = Convert.ToInt32(Request.QueryString["idPregunta"]);
            int idResultadoEvaluacion = Convert.ToInt32(Request.QueryString["idResultadoEvaluacion"]);

            EvaluadorServicio.EvaluacionDeRespuesta(idRespuesta,idResultadoEvaluacion);

            EmailAAlumnosServicio correo = new EmailAAlumnosServicio();
            correo.GenerarEmailResultadoEvaluacion(idRespuesta);

            return RedirectToAction("EvaluarRespuesta", "Profesor", new { idPregunta = idPreg, filtro = -1 });
        }
    }
}