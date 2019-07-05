using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Servicios
{
    public class EmailServicio
    {
        protected MailMessage email;
        protected SmtpClient smtpClient;

        //Constructor
        public EmailServicio()
        {
            this.email = new MailMessage();
            this.email.From = new MailAddress("ProgWeb3TPFinal.Unlam@gmail.com"); // Dirección email de la aplicación
            this.email.IsBodyHtml = true;
            this.email.Priority = MailPriority.Normal;

            this.smtpClient = new SmtpClient();
            this.smtpClient.Host = "smtp.gmail.com";//	Dirección URL del servidor de correo SMTP 
            this.smtpClient.Port = 587;//Número del puerto de comunicaciones utilizado por el protocolo SMTP.
            this.smtpClient.EnableSsl = true;//Conmutador de habilitación de seguridad SSL –Secure Sockets Layer, capa de conexión segura-
            this.smtpClient.UseDefaultCredentials = false;
            this.smtpClient.Credentials = new NetworkCredential(this.email.From.Address, "ProgWeb3TPFinal");//password del mail programacionweb3
        }

        private void EnviarA(List<String> destinatarios)
        {
            foreach (string destinatario in destinatarios)
            {
                this.email.To.Add(new MailAddress(destinatario));
            }
            smtpClient.Send(this.email); //Se trata del método que se ha de llamar para el envío del email, tiene la sintaxis: Send(destinatario, cabecera, contenido[, adjuntos])
            this.email.Dispose();
        }

        private void EnviarA(string destinatario)
        {
            this.email.To.Add(new MailAddress(destinatario));
            smtpClient.Send(this.email);
            this.email.Dispose();
        }

        public void EnviarMailRepuestaProfesores(int idPregunta, int idUsuario, List<String> emailsDestinos)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            RespuestaAlumno ra = ctx.RespuestaAlumno.Where(o => o.IdPregunta == idPregunta && o.IdAlumno == idUsuario).FirstOrDefault();

            string servidor = "http://" + HttpContext.Current.Request.Url.Host + ":"
                + HttpContext.Current.Request.Url.Port;
            //ASUNTO
            this.email.Subject = "Respuesta a Pregunta " + ra.Pregunta.Nro + " - " + ra.Orden + " - " + ra.Alumno.Apellido;

            //CUERPO DEL MENSAJE
            email.Body = "Pregunta: " + ra.Pregunta.Pregunta1 + "<br> Alumno: " + ra.Alumno.Nombre + " " + 
                ra.Alumno.Apellido + "<br> Orden: " + ra.Orden + "<br> Respuesta: " + ra.Respuesta + 
                "<br> Evaluar:" + servidor + "/Profesor/EvaluarRespuestas/" + ra.IdRespuestaAlumno;

            this.EnviarA(emailsDestinos);
        }

        public void GenerarEmailMejorRespuesta(int idRespuesta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno respuesta = ctx.RespuestaAlumno.Where(x => x.IdRespuestaAlumno == idRespuesta).FirstOrDefault();

            this.email.Subject = "Su respuesta ha sido marcada como la mejor. ¡Felicitaciones!";

            string servidor = "http://" + HttpContext.Current.Request.Url.Host + ":"
                + HttpContext.Current.Request.Url.Port;
            this.email.Body =
                "Su respuesta ha sido marcada como la mejor. <br>" +
                "Pregunta: " + respuesta.Pregunta.Pregunta1 + "<br>" +
                "Su Respuesta: " + respuesta.Respuesta +
                " <a href='" + servidor + "/Alumnos/VerRespuesta?id="
                + respuesta.IdPregunta + "'>Link</a> <br>" +
                "Posiciones: <a href='" + servidor + "/Home/inicio'>Link</a>"
                 + "<br> ¡Felicitaciones!";

            this.EnviarA(respuesta.Alumno.Email);

        }

        public void GenerarEmailResultadoEvaluacion(int idRespuesta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno respuesta = ctx.RespuestaAlumno.Where(x => x.IdRespuestaAlumno == idRespuesta).FirstOrDefault();

            this.email.Subject = "Su respuesta fue calificada como " + respuesta.ResultadoEvaluacion.Resultado;

            string servidor = "http://" + HttpContext.Current.Request.Url.Host + ":"
                + HttpContext.Current.Request.Url.Port;
            this.email.Body =
                "Pregunta: " + respuesta.Pregunta.Pregunta1 + "<br>" +
                "Su Respuesta: " + respuesta.Respuesta +
                " <a href='" + servidor + "/Alumnos/VerRespuesta?id="
                + respuesta.IdPregunta + "'>Link</a> <br>" +
                "Posiciones: <a href='" + servidor + "/Home/inicio'>Link</a>"
                 + "<br> ¡Felicitaciones!";

            this.EnviarA(respuesta.Alumno.Email);

        }
    }
    
}
