using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using Entidades.ModelView;

namespace Servicios
{
    public class RespuestasServicio
    {
        /*
         * Obtiene todas las respuestas a una pregunta en particular
         */
        private static List<RespuestaAlumno> ObtenerRespuestasAPregunta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<RespuestaAlumno> respuestas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta).OrderBy(x => x.FechaHoraRespuesta).ToList();

            return respuestas;
        }

        /*
         * Obtiene una lista con todas las respuestas sin corregir de una pregunta en particular
         */
        private static List<RespuestaAlumno> ObtenerRespuestasSinCorregirAPregunta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<RespuestaAlumno> respuestas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta && x.IdResultadoEvaluacion == null).OrderBy(x => x.FechaHoraRespuesta).ToList();

            return respuestas;
        }

        /*
         * Obtiene una lista con todas las respuestas de acuerdo a la correccion especificada
         * (idResultadoEvaluacion) de una pregunta en particular
         */
        private static List<RespuestaAlumno> ObtenerRespuestasCorregidasDePregunta(int idPregunta, int idResultadoEvaluacion)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<RespuestaAlumno> respuestas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta && x.IdResultadoEvaluacion == idResultadoEvaluacion).OrderBy(x => x.FechaHoraRespuesta).ToList();

            return respuestas;
        }

        /*
         * Retorna true si todas las respuestas de una pregunta en particular fueron corregidas
         */
        private static bool VerificarSiTodasLasRespuestasEstanCorregidas(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            int respuestasSinCorregir = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta && x.IdResultadoEvaluacion == null).Count();
            if (respuestasSinCorregir > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*
         * Retorna true si una pregunta tiene una respuesta marcada como mejor respuesta
         */
        private static bool VerificarSiExisteMejorRespuesta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno mejorRespuesta = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta && x.MejorRespuesta).FirstOrDefault();
            if (mejorRespuesta != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static EvaluarRepuestaViewModel ObtenerRespuestaPorFiltro(int idPregunta, int filtro)
        {
            EvaluarRepuestaViewModel respuestas = new EvaluarRepuestaViewModel();

            switch (filtro)
            {
                case 0:
                    respuestas.RepuestasAlumnos = ObtenerRespuestasSinCorregirAPregunta(idPregunta);
                    break;
                case 1:
                case 2:
                case 3:
                    respuestas.RepuestasAlumnos = ObtenerRespuestasCorregidasDePregunta(idPregunta, filtro);
                    break;
                default:
                    respuestas.RepuestasAlumnos = ObtenerRespuestasAPregunta(idPregunta);
                    break;
            }

            respuestas.Pregunta = PreguntasServicio.ObtenerPreguntaporId(idPregunta);
            respuestas.TodasCorregidas = VerificarSiTodasLasRespuestasEstanCorregidas(idPregunta);
            respuestas.MejorRepuesta = VerificarSiExisteMejorRespuesta(idPregunta);

            return respuestas;
        }

        public static void guardarRespuesta(RespuestaAlumno ra, int idUsuario)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            //Busco las preguntas con el mismo id para determinar el orden
            int orden = ctx.RespuestaAlumno.Where(o => o.IdPregunta == ra.IdPregunta).Count();
            orden = orden + 1;
            RespuestaAlumno nuevo = new RespuestaAlumno
            {
                Orden = orden,
                FechaHoraRespuesta = DateTime.Now,
                IdAlumno = idUsuario,
                IdPregunta = ra.IdPregunta,
                Respuesta = ra.Respuesta
            };
            ctx.RespuestaAlumno.Add(nuevo);

            ctx.SaveChanges();

            //envio email a profesores
            List<String> emial_profesores = ctx.Profesor.Select(x => x.Email).ToList();
            EmailServicio email = new EmailServicio();
            email.EnviarMailRepuestaProfesores(ra.IdPregunta, idUsuario, emial_profesores);
        }

        public static RespuestaAlumno verRespuesta(int idAlumno, int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno ra = ctx.RespuestaAlumno.Where(a => a.IdPregunta == idPregunta && a.IdAlumno == idAlumno).FirstOrDefault();
            return ra;
        }
    }
}
