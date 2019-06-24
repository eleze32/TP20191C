using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.ModelsDTO
{
    public class ListarVerPreguntas
    {
        public int IdPregunta { get; set; }
        public int Nro { get; set; }
        public String Pregunta { get; set; }
        public String Clase { get; set; }
        public String Resultado { get; set; }
        public DateTime? Fecha { get; set; }
        public String Tema { get; set; }
        public int Orden { get; set; }
        public long? Puntos { get; set; }
        public bool MejorRespuesta { get; set; }
    }
}
