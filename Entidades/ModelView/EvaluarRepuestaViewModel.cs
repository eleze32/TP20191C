using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.ModelView
{
    public class EvaluarRepuestaViewModel
    {
        public List<RespuestaAlumno> RepuestasAlumnos { get; set; }
        public Pregunta Pregunta { get; set; }
        public bool TodasCorregidas { get; set; }
        public bool MejorRepuesta { get; set; }
    }
}
