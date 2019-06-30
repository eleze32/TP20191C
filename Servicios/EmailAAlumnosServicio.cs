using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Servicios
{
    public class EmailAAlumnosServicio : EmailServicio
    {
        public void GenerarEmailMejorRespuesta(int idRespuesta)
        {
            base.mail.Subject = "Su respuesta ha sido marcada como la mejor. ¡Felicitaciones!";

            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno respuesta = ctx.RespuestaAlumno.Where(x => x.IdRespuestaAlumno == idRespuesta).FirstOrDefault();

            string servidor = "http://"+HttpContext.Current.Request.Url.Host+":"
                + HttpContext.Current.Request.Url.Port;
            base.mail.Body = 
                "Su respuesta ha sido marcada como la mejor. <br>" +
                "Pregunta: " + respuesta.Pregunta.Pregunta1 + "<br>" +
                "Su Respuesta: "+ respuesta.Respuesta +
                " <a href='"+ servidor+ "/Alumnos/verRespuesta?id="
                + respuesta.IdPregunta+ "'>Link</a> <br>"  +
                "Posiciones: <a href='"+servidor+ "/Home/inicio'>Link</a>"
                 + "<br> ¡Felicitaciones!";

            base.EnviarA(respuesta.Alumno.Email);

        }

        public void GenerarEmailResultadoEvaluacion(int idRespuesta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno respuesta = ctx.RespuestaAlumno.Where(x => x.IdRespuestaAlumno == idRespuesta).FirstOrDefault();

            base.mail.Subject = "Su respuesta fue calificada como " + respuesta.ResultadoEvaluacion.Resultado;

            string servidor = "http://" + HttpContext.Current.Request.Url.Host + ":"
                + HttpContext.Current.Request.Url.Port;
            base.mail.Body = 
                "Pregunta: " + respuesta.Pregunta.Pregunta1 + "<br>" +
                "Su Respuesta: " + respuesta.Respuesta +
                " <a href='" + servidor + "/Alumnos/verRespuesta?id="
                + respuesta.IdPregunta + "'>Link</a> <br>" +
                "Posiciones: <a href='" + servidor + "/Home/inicio'>Link</a>"
                 + "<br> ¡Felicitaciones!";

            base.EnviarA(respuesta.Alumno.Email);

        }

    }
}
