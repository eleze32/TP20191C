using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entidades;

namespace Servicios
{
    public class UsuarioServicio
    {
        public static bool ingresar(string email, string pass, bool soyProf)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            if (soyProf)
            {
                Profesor prof = ctx.Profesor.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();

                if (prof != null)
                {
                    HttpContext.Current.Session["Usuario"] = String.Format("{0} {1}", prof.Nombre, prof.Apellido);
                    HttpContext.Current.Session["UsuarioId"] = prof.IdProfesor;
                    return true;
                }

            }
            else
            {
                Alumno al = ctx.Alumno.Where(x => x.Email == email && x.Password == pass).FirstOrDefault();

                if (al != null)
                {
                    HttpContext.Current.Session["Usuario"] = String.Format("{0} {1}", al.Nombre, al.Apellido);
                    HttpContext.Current.Session["UsuarioId"] = al.IdAlumno;
                    return true;
                }
            }

            return false;
        }
    }
}
