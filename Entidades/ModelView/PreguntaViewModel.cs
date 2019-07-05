using System;
using System.Collections.Generic;

namespace Entidades.ModelView
{
    public class PreguntaViewModel
    {
        public Pregunta Pregunta { get; set; }
        public List<Tema> Tema { get; set; }
        public List<Clase> Clase { get; set; }
    }
}
