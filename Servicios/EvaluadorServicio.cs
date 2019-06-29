using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entidades;

namespace Servicios
{
    public class EvaluadorServicio
    {
        /*
         * Obtiene el mejor puntaje que hay para una pregunta en particular
         */
        public static long ObtenerMejorPuntaje(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            long mayorPuntaje = (long)ctx.RespuestaAlumno.Where(x => x.IdPregunta == idPregunta).Max(x => x.Puntos);

            return mayorPuntaje;
        }

        /*
         * Actualiza la tabla RespuestaAlumno para asignar una mejor respuesta, con su correspondiente puntaje
         */
        public static void MarcarComoMejorRespuesta(int idRespuesta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno mejorRespuesta = mejorRespuesta = ctx.RespuestaAlumno.Where(x => x.IdRespuestaAlumno == idRespuesta).FirstOrDefault();
            mejorRespuesta.MejorRespuesta = true;
            long mayorPuntaje = ObtenerMejorPuntaje(mejorRespuesta.IdPregunta);
            mejorRespuesta.Puntos = mejorRespuesta.Puntos + (mayorPuntaje / 2);
            ctx.SaveChanges();
            return;
        }

        /*
         * Actualiza la tabla RespuestaAlumno con la evaluacion asignada y con su correspondiente puntaje
         */
        public static void EvaluacionDeRespuesta(int idRespuesta, int idResultadoEvaluacion)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno respuesta = ctx.RespuestaAlumno.Where(x => x.IdRespuestaAlumno == idRespuesta).FirstOrDefault();
            int respCorrectas = ctx.RespuestaAlumno.Where(x => x.IdPregunta == respuesta.IdPregunta && x.IdResultadoEvaluacion == 1).Count();
            ResultadoEvaluacion resEvaluacion = ctx.ResultadoEvaluacion.Where(x => x.IdResultadoEvaluacion == idResultadoEvaluacion).FirstOrDefault();

            respuesta.RespuestasCorrectasHastaElMomento = respCorrectas;
            respuesta.ResultadoEvaluacion = resEvaluacion;
            respuesta.IdProfesorEvaluador = (int) HttpContext.Current.Session["UsuarioId"];
            respuesta.FechaHoraEvaluacion = DateTime.Now;

            int puntajeMax = Convert.ToInt32(ConfigurationManager.AppSettings["PuntajeMaximoPorRespuestaCorrecta"]);
            int cupo = Convert.ToInt32(ConfigurationManager.AppSettings["CupoMaximoRespuestasCorrectas"]);
            int puntajeRespuesta;

            switch (idResultadoEvaluacion)
            {
            case 1: //Correcta
                puntajeRespuesta = puntajeMax - ( (puntajeMax / cupo) * respCorrectas);
                if(puntajeRespuesta <= 0)
                {
                    respuesta.Puntos = puntajeMax / cupo;
                }
                else
                {
                    respuesta.Puntos = puntajeRespuesta;
                }
                Alumno alumnoCorregido1 = ctx.Alumno.Where(x => x.IdAlumno == respuesta.IdAlumno).FirstOrDefault();
                alumnoCorregido1.PuntosTotales = alumnoCorregido1.PuntosTotales + puntajeRespuesta;
                break;
            case 2: //Regular
                puntajeRespuesta = (puntajeMax - ( (puntajeMax / cupo) * respCorrectas)) / 2;
                if (puntajeRespuesta <= 0)
                {
                    respuesta.Puntos = (puntajeMax / cupo) / 2;
                }
                else
                {
                    respuesta.Puntos = puntajeRespuesta;
                }
                Alumno alumnoCorregido2 = ctx.Alumno.Where(x => x.IdAlumno == respuesta.IdAlumno).FirstOrDefault();
                alumnoCorregido2.PuntosTotales = alumnoCorregido2.PuntosTotales + puntajeRespuesta;
                break;
            case 3: //Mal
                respuesta.Puntos = 0;
                break;
            }

            ctx.SaveChanges();
            return;
        }
    }
}
