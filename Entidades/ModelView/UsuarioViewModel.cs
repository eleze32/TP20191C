using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades.ModelView
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage ="Por favor ingresar {0}.")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Por favor ingresar {0}.")]
        public string Password { get; set; }
        [Display(Name ="Soy Profesor")]
        public bool SoyProf { get; set; }
    }
}
