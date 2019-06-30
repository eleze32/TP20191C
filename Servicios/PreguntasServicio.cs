using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicios
{
    public class PreguntasServicio
    {
        /*
         * Obtiene las ultimas dos preguntas donde la fechadisponiblehasta es menir a la fechahora actual
         */
        public static List<Pregunta> UltimasDospreguntas()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<Pregunta> preguntas = ctx.Pregunta.Where(x => x.FechaDisponibleHasta < DateTime.Now).OrderByDescending(x => x.Nro).Take(2).ToList();

            return preguntas;
        }

        /*
         * Obtiene las preguntas realizadas por un profesor en específico         
         */
         public static List<Pregunta> ObtenerPreguntas()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<Pregunta> preguntas = ctx.Pregunta.ToList();

            return preguntas;
        }

        public static Pregunta ObtenerInformacionDePregunta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta pregunta = ctx.Pregunta.Where(x => x.IdPregunta == idPregunta).FirstOrDefault();

            return pregunta;
        }

        public static RespuestaAlumno verRespuesta(int idAlumno, int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno ra = ctx.RespuestaAlumno.Where(a => a.IdPregunta == idPregunta && a.IdAlumno == idAlumno).FirstOrDefault();
            return ra;
        }

        public static bool verFechaPreguntaValida(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta p = ctx.Pregunta.Where(a => a.IdPregunta == idPregunta).FirstOrDefault();
            if (p.FechaDisponibleHasta == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool plazoFecha(int idPregunta)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta p = ctx.Pregunta.Where(a => a.IdPregunta == idPregunta).FirstOrDefault();

            DateTime fechaHasta = p.FechaDisponibleHasta ?? DateTime.Now;

            if (p.FechaDisponibleHasta == null)
            {
                return true;
            }
            else
            {
                int fecha = DateTime.Compare(fechaHasta, DateTime.Now);
                if (fecha <= 0)
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
}
