using System.Collections.Generic;
using System.Web.Mvc;
using Entidades;
using Entidades.ModelView;
using Servicios;

namespace TP20191C.Controllers
{
    [Authorize]
    public class AlumnosController : Controller
    {
        // alumno ve su repuesta
        public ActionResult VerRespuesta(int id)
        {
            Pregunta pregunta = PreguntasServicio.ObtenerPreguntaporId(id);
            int verifica_plazo_fecha = PreguntasServicio.VerifcaPlazoFecha(pregunta),
                id_usuario = (int)Session["UsuarioId"];
            
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Profesor")
                return RedirectToAction("Error", "Home", new { @error = 404 });
            
            if (pregunta == null)
                return RedirectToAction("VerPreguntas", "Alumnos");
            
            //Seguridad: Verifico que la Pregunta tenga fecha de respuesta, en caso contrario redirijo a Preguntas
            if ( verifica_plazo_fecha == -1)
                return RedirectToAction("VerPreguntas", "Alumnos");

            RespuestaAlumno ra = RespuestasServicio.verRespuesta(id_usuario, id);
            
            /*Verifico que haya repueta, si es null no hay repuesta, y verifico el plazo de repuesta para ver si vencio,
             y le paso los datos de la pregunta a la repuesta*/

            if(ra == null)
            {
                ra = new RespuestaAlumno { Pregunta = pregunta };
                ViewBag.SinRepuesta = true;
                //Verifico si se vencio el plazo de la pregunta
                if(verifica_plazo_fecha == 1)
                    ViewBag.plazo = verifica_plazo_fecha;
            }else
                ViewBag.SinRepuesta = false;

            return View(ra);
        }

        // Alumnos ve todas las preguntas, ya sea que no respondio, si respondio, y sus calificaiones
        public ActionResult VerPreguntas(int filtro = -1)
        {
            int id_usuario = (int)Session["UsuarioId"];

            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Profesor")
                return RedirectToAction("Error", "Home", new { @error = 404 });

            if (filtro < -1 || filtro > 3)
                return RedirectToAction("Inicio", "Home");

            ViewBag.NroFiltro = filtro;
            List<ListarVerPreguntas> pregunta = PreguntasServicio.ObtenerPreguntasPorFiltro(id_usuario,filtro);
            
            return View(pregunta);
        }

        // Alumno responde pregunta
        public ActionResult ResponderPregunta(int id)
        {
            Pregunta pregunta = PreguntasServicio.ObtenerPreguntaporId(id);
            int verifica_plazo_fecha = PreguntasServicio.VerifcaPlazoFecha(pregunta),
                id_usuario = (int)Session["UsuarioId"];

            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Profesor")
                return RedirectToAction("Error", "Home", new { @error = 404 });
            
            //Seguridad: Verifico que la Pregunta no haya sido respondida, en caso contrario redirijo a VerRespuesta
            if (PreguntasServicio.verPreguntaValidaAlumno(id_usuario, id))
                return RedirectToAction("VerRespuesta", "Alumnos",new { id = id});
            
            if(verifica_plazo_fecha == -1)
                return RedirectToAction("VerPreguntas", "Alumnos");

            ViewBag.Pregunta = pregunta;
            ViewBag.Fecha = verifica_plazo_fecha;
            
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ResponderPregunta(RespuestaAlumno ra)
        {
            int id_usuario = (int)Session["UsuarioId"];

            if (Session["UsuarioId"] == null)
                return RedirectToAction("Ingresar", "Home");

            if ((string)Session["TipoUsuario"] == "Profesor")
                return RedirectToAction("Error", "Home", new { @error = 404 });
            
            if (!ModelState.IsValid)
            {
                Pregunta pregunta = PreguntasServicio.ObtenerPreguntaporId(ra.IdPregunta);

                ViewBag.Pregunta = pregunta;
                ViewBag.Fecha = PreguntasServicio.VerifcaPlazoFecha(pregunta);
                
                return View(ra);
            }
            
            RespuestasServicio.guardarRespuesta(ra, id_usuario);
            return RedirectToAction("VerPreguntas", "Alumnos");
        }

        [ActionName("acerca-de")]
        public ActionResult AcercaDe(string layout = "~/Views/Shared/AlumnosLayout.cshtml")
        {
            ViewBag.layout = layout;
            return View();
        }
    }
}