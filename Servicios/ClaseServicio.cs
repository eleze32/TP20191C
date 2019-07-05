using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicios
{
    public class ClaseServicio
    {
        public static List<Clase> ObtenerTodasLasClases()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            List<Clase> Lista_Clases = ctx.Clase.ToList();

            return Lista_Clases;
        }
    }
}
