using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entidades;

namespace Servicios
{
    public class ABMPreguntasServicio
    {
        public static bool Crear(Pregunta p)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            if (PreguntasServicio.ExisteNumeroDePregunta(p.Nro, ctx))
                return false;

            p.FechaHoraCreacion = DateTime.Now;
            p.IdProfesorCreacion = (int)HttpContext.Current.Session["UsuarioId"];
            ctx.Pregunta.Add(p);
            ctx.SaveChanges();

            return true;
        }

        public static bool Modificar(Pregunta pregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            Pregunta p = ctx.Pregunta.Find(pregunta.IdPregunta);

            if (!PreguntasServicio.ExisteNumeroDePregunta(pregunta.Nro, ctx))
            {
                p.Nro = pregunta.Nro;
            }
            else
            {
                Pregunta p1 = ctx.Pregunta.Where(x => x.Nro == pregunta.Nro).First();

                if (p1.IdPregunta != pregunta.IdPregunta)
                    return false;
            }

            p.IdClase = pregunta.IdClase;
            p.FechaHoraModificacion = DateTime.Now;
            p.FechaDisponibleDesde = pregunta.FechaDisponibleDesde;
            p.FechaDisponibleHasta = pregunta.FechaDisponibleHasta;
            p.IdProfesorModificacion = (int)HttpContext.Current.Session["UsuarioId"];
            p.IdTema = pregunta.IdTema;
            p.Pregunta1 = pregunta.Pregunta1;

            ctx.SaveChanges();

            return true;
        }

        public static int Eliminar(int id)
        {
            // retorna 1 si elimina, 0 si no existe pregunta, -1 si tiene repuestas
            int eliminado = -1;

            using (TP_20191CEntities ctx = new TP_20191CEntities())
            {
                Pregunta p = ctx.Pregunta.Find(id);

                if (p == null)
                    eliminado = 0;
                else
                {
                    if (p.RespuestaAlumno.Count <= 0)
                    {
                        ctx.Pregunta.Remove(p);
                        ctx.SaveChanges();
                        eliminado = 1;
                    }
                }
            }

            return eliminado;
        }

    }
}
