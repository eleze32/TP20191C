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

            EmailServicio correo = new EmailServicio();
            correo.GenerarEmailMejorRespuesta(idRespuesta);

            return RedirectToAction("EvaluarRespuestas", "Profesor", new { id = idPreg, filtro = -1 });            
        }

        // profesor evalua una respuesta como correcta, regular o mal
        public ActionResult EvaluarRespuesta(int idRespuesta, int idPregunta,int idResultadoEvaluacion)
        {
            EvaluadorServicio.EvaluacionDeRespuesta(idRespuesta,idResultadoEvaluacion);

            EmailServicio correo = new EmailServicio();
            correo.GenerarEmailResultadoEvaluacion(idRespuesta);

            return RedirectToAction("EvaluarRespuestas", "Profesor", new { id = idPregunta, filtro = -1 });
        }
    }
}