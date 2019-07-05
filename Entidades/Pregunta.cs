//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Pregunta:IValidatableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pregunta()
        {
            this.RespuestaAlumno = new List<RespuestaAlumno>();
        }
    
        public int IdPregunta { get; set; }
        [Required(ErrorMessage ="Por favor elegir un {0} de pregunta.")]
        [Display(Name ="Numero de pregunta")]
        public int Nro { get; set; }
        [Required(ErrorMessage ="Por favor de elegir una {0}.")]
        [Display(Name ="Clase")]
        public int IdClase { get; set; }
        [Required(ErrorMessage = "Por favor de elegir un {0}.")]
        [Display(Name = "Tema")]
        public int IdTema { get; set; }
        [Display(Name ="Fecha Inicio")]
        public Nullable<System.DateTime> FechaDisponibleDesde { get; set; }
        [Display(Name ="Fecha final")]
        public Nullable<System.DateTime> FechaDisponibleHasta { get; set; }
        [Required(ErrorMessage = "Por favor de elegir una {0}.")]
        [Display(Name = "Pregunta")]
        public string Pregunta1 { get; set; }
        public int IdProfesorCreacion { get; set; }
        public System.DateTime FechaHoraCreacion { get; set; }
        public Nullable<int> IdProfesorModificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
    
        public virtual Clase Clase { get; set; }
        public virtual Profesor Profesor { get; set; }
        public virtual Profesor Profesor1 { get; set; }
        public virtual Tema Tema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<RespuestaAlumno> RespuestaAlumno { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            Nullable<DateTime> fecha_actual = DateTime.Now;

            if (FechaDisponibleDesde == null && FechaDisponibleHasta != null)
            {
                errores.Add(new ValidationResult("La fecha disponible desde tiene que tener una fecha.",
                  new string[] { "FechaDisponibleDesde" }));
            }

            if (FechaDisponibleDesde != null && FechaDisponibleHasta == null)
            {
                errores.Add(new ValidationResult("La fecha disponible hasta tiene que tener una fecha.",
                  new string[] { "FechaDisponibleHasta" }));
            }

            if (FechaDisponibleDesde != null && Nullable.Compare<DateTime>(FechaDisponibleDesde, fecha_actual) < 0)
            {
                errores.Add(new ValidationResult("La fecha disponible desde tiene que ser mayor o igual a la fecha actual.",
                    new string[] { "FechaDisponibleDesde" }));

            }

            if (FechaDisponibleDesde != null && FechaDisponibleHasta != null &&
                Nullable.Compare<DateTime>(FechaDisponibleDesde, FechaDisponibleHasta) >= 0)
            {
                errores.Add(new ValidationResult("La fecha disponible hasta tiene que ser mayor a fecha disnible desde.",
                    new string[] { "FechaDisponibleHasta" }));
            }

            return errores;
        }
    }
}
