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
        public static List<Pregunta> ultimasDospreguntas()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List<Pregunta> preguntas = ctx.Pregunta.Where(x => x.FechaDisponibleHasta < DateTime.Now).OrderByDescending(x => x.Nro).Take(2).ToList();

            return preguntas;
        }
    }
}
