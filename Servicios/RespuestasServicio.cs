using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;

namespace Servicios
{
    public class RespuestasServicio
    {
        /*
         * Obtiene todas las respuestas a una pregunta en particular
         */
        public static List<RespuestaAlumno> ObtenerRespuestasAPregunta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<RespuestaAlumno> respuestas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta).OrderBy(x => x.FechaHoraRespuesta).ToList();

            return respuestas;
        }

        /*
         * Obtiene una lista con todas las respuestas sin corregir de una pregunta en particular
         */
        public static List<RespuestaAlumno> ObtenerRespuestasSinCorregirAPregunta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<RespuestaAlumno> respuestas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta && x.IdResultadoEvaluacion == null).OrderBy(x => x.FechaHoraRespuesta).ToList();

            return respuestas;
        }

        /*
         * Obtiene una lista con todas las respuestas de acuerdo a la correccion especificada
         * (idResultadoEvaluacion) de una pregunta en particular
         */
        public static List<RespuestaAlumno> ObtenerRespuestasCorregidasDePregunta(int idPregunta, int idResultadoEvaluacion)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<RespuestaAlumno> respuestas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta && x.IdResultadoEvaluacion == idResultadoEvaluacion).OrderBy(x => x.FechaHoraRespuesta).ToList();

            return respuestas;
        }

        /*
         * Retorna true si todas las respuestas de una pregunta en particular fueron corregidas
         */
        public static bool VerificarSiTodasLasRespuestasEstanCorregidas(int idPregunta)
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
        public static bool VerificarSiExisteMejorRespuesta(int idPregunta)
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
    }
}
