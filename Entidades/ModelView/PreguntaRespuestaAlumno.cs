﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.ModelView
{
    public class PreguntaRespuestaAlumno
    {
        public int Nro { get; set; }
        public String Pregunta { get; set; }
        public int IdAlumno { get; set; }
        public int IdPregunta { get; set; }
    }
}