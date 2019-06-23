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

            //Falta enviar mail

            return RedirectToAction("EvaluarRespuesta", "Profesor", new { idPregunta = idPreg, filtro = -1 });            
        }

        // profesor marca una respuesta como la mejor
        public ActionResult EvaluarRespuesta()
        {
            int idRespuesta = Convert.ToInt32(Request.QueryString["idRespuesta"]);
            int idPreg = Convert.ToInt32(Request.QueryString["idPregunta"]);
            int idResultadoEvaluacion = Convert.ToInt32(Request.QueryString["idResultadoEvaluacion"]);

            EvaluadorServicio.EvaluacionDeRespuesta(idRespuesta,idResultadoEvaluacion);

            //Falta enviar mail

            return RedirectToAction("EvaluarRespuesta", "Profesor", new { idPregunta = idPreg, filtro = -1 });
        }
    }
}