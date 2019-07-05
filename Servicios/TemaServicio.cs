using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Servicios
{
    public class TemaServicio
    {
        public static List<Tema> ObtenerTodosLosTemas()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            List<Tema> Lista_Temas = ctx.Tema.ToList();

            return Lista_Temas;
        }
    }
}
