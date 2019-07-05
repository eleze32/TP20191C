using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entidades;
using Entidades.ModelView;

namespace Servicios
{
    public class UsuarioServicio
    {
        

        public static bool Ingresar(UsuarioViewModel usuario)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            if (usuario.SoyProf)
            {
                Profesor prof = ctx.Profesor.Where(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();

                if (prof != null)
                {
                    HttpContext.Current.Session["Usuario"] = String.Format("{0} {1}", prof.Nombre,prof.Apellido);
                    HttpContext.Current.Session["UsuarioId"] = prof.IdProfesor;
                    HttpContext.Current.Session["TipoUsuario"] = "Profesor";
                    return true;
                }

            }
            else{
                Alumno al = ctx.Alumno.Where(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();

                if (al != null)
                {
                    HttpContext.Current.Session["Usuario"] = String.Format("{0} {1}", al.Nombre, al.Apellido);
                    HttpContext.Current.Session["UsuarioId"] = al.IdAlumno;
                    HttpContext.Current.Session["TipoUsuario"] = "Alumno";
                    return true;
                }
            }

            return false;
        }
    }
}
