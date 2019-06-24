using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP20191C.Models
{
    public class Usuario
    {
        [DataType(DataType.EmailAddress), Required(ErrorMessage = "El campo {0} es requerido"), Display(Name = "Email"), StringLength(400, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracteres")]
        public String Email { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "El campo {0} es requerido"), Display(Name = "Password"), StringLength(400, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracteres")]
        public String Password { get; set; }
    }
}