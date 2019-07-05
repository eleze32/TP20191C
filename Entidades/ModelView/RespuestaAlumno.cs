using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entidades
{
    [MetadataType(typeof(RespuestaAlumnoMetaData))]
    public partial class RespuestaAlumno
    {
    }

    public class RespuestaAlumnoMetaData
    {
        [Required(ErrorMessage = "Por favor debe ingresar una repuesta.")]
        [AllowHtml]
        public string Respuesta { get; set; }

    }
}
