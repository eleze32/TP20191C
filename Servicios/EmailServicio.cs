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
    class EmailServicio
    {

        public static void enviarEmail(int idPregunta,int idUsuario,String emailDestino) {

            TP_20191CEntities ctx = new TP_20191CEntities();

            RespuestaAlumno ra = ctx.RespuestaAlumno.Where(o => o.IdPregunta == idPregunta && o.IdAlumno == idUsuario).FirstOrDefault();
            List<Profesor> p = ctx.Profesor.ToList();

            MailMessage email = new MailMessage();

            // EMAILS DE PROFESORES, USAMOS UN FOREACH PARA AGREGAR TODOS
            email.To.Add(new MailAddress(emailDestino));

            //EMAIL DE LA APLICACION
            email.From = new MailAddress("ProgWeb3TPFinal.Unlam@gmail.com");

            //ASUNTO
            email.Subject = "Respuesta a Pregunta "+ ra.Pregunta.Nro +" - "+ ra.Orden +" - "+ ra.Alumno.Apellido;

            //CUERPO DEL MENSAJE
            email.Body = "Pregunta: " + ra.Pregunta.Pregunta1 + "<br> Alumno: " + ra.Alumno.Nombre +" "+ ra.Alumno.Apellido +"<br> Orden: " + ra.Orden +"<br> Respuesta: " + ra.Respuesta +"<br> Evaluar:"+ "http://localhost/Profesor/EvaluarRespuesta/"+ra.IdRespuestaAlumno;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();


            smtp.Host = "smtp.gmail.com";//	Dirección URL del servidor de correo SMTP 
            smtp.Port = 587;//Número del puerto de comunicaciones utilizado por el protocolo SMTP.


            smtp.EnableSsl = true;//Conmutador de habilitación de seguridad SSL –Secure Sockets Layer, capa de conexión segura-
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(email.From.Address, "ProgWeb3TPFinal");//password del mail programacionweb3

            smtp.Send(email); //Se trata del método que se ha de llamar para el envío del email, tiene la sintaxis: Send(destinatario, cabecera, contenido[, adjuntos])
            email.Dispose();
        }
    }
    
}
