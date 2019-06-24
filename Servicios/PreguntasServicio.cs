using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Servicios.ModelsDTO;
using TP20191C.Models;

namespace Servicios
{
    public class PreguntasServicio
    {
        /*
         * Obtiene las ultimas dos preguntas donde la fechadisponiblehasta es menir a la fechahora actual
         */
        public static List<Pregunta> ultimasDospreguntas()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            List<Pregunta> preguntas = ctx.Pregunta.Where(x => x.FechaDisponibleHasta < DateTime.Now).OrderByDescending(x => x.Nro).Take(2).ToList();

            return preguntas;
        }

        public static List<PreguntaRespuestaAlumno> preguntasSinResponder(int id){

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join pr in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id) on p.IdPregunta equals pr.IdPregunta into gj
                        from x in gj.DefaultIfEmpty()
                        select new PreguntaRespuestaAlumno
                        {

                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            IdAlumno = (x == null ? 0 : x.IdAlumno)
                        };

            return query.Where(a => a.IdAlumno == 0).ToList();
           
        }

        public static List<ListarVerPreguntas> preguntasMal(int id) {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id && a.IdResultadoEvaluacion == 3) on p.IdPregunta equals ra.IdPregunta
                        join c in ctx.Clase on p.IdClase equals c.IdClase
                        join re in ctx.ResultadoEvaluacion on ra.IdResultadoEvaluacion equals re.IdResultadoEvaluacion
                        join t in ctx.Tema on p.IdTema equals t.IdTema
                        select new ListarVerPreguntas{
                            IdPregunta = p.IdPregunta,
                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            Clase = c.Nombre,
                            Resultado = re.Resultado,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();

        }

        public static List<ListarVerPreguntas> preguntasCorrectas(int id)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id && a.IdResultadoEvaluacion == 1) on p.IdPregunta equals ra.IdPregunta
                        join c in ctx.Clase on p.IdClase equals c.IdClase
                        join re in ctx.ResultadoEvaluacion on ra.IdResultadoEvaluacion equals re.IdResultadoEvaluacion
                        join t in ctx.Tema on p.IdTema equals t.IdTema
                        select new ListarVerPreguntas
                        {
                            IdPregunta = p.IdPregunta,
                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            Clase = c.Nombre,
                            Resultado = re.Resultado,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();

        }

        public static List<ListarVerPreguntas> preguntasRegular(int id)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id && a.IdResultadoEvaluacion == 2) on p.IdPregunta equals ra.IdPregunta
                        join c in ctx.Clase on p.IdClase equals c.IdClase
                        join re in ctx.ResultadoEvaluacion on ra.IdResultadoEvaluacion equals re.IdResultadoEvaluacion
                        join t in ctx.Tema on p.IdTema equals t.IdTema
                        select new ListarVerPreguntas
                        {
                            IdPregunta = p.IdPregunta,
                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            Clase = c.Nombre,
                            Resultado = re.Resultado,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();

        }

        public static List<ListarVerPreguntas> preguntasSinCorregir(int id)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id && a.IdResultadoEvaluacion == 0) on p.IdPregunta equals ra.IdPregunta
                        join c in ctx.Clase on p.IdClase equals c.IdClase
                        join re in ctx.ResultadoEvaluacion on ra.IdResultadoEvaluacion equals re.IdResultadoEvaluacion
                        join t in ctx.Tema on p.IdTema equals t.IdTema
                        select new ListarVerPreguntas
                        {
                            IdPregunta = p.IdPregunta,
                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            Clase = c.Nombre,
                            Resultado = re.Resultado,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();

        }

        public static List<ListarVerPreguntas> preguntasTodas(int id)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id) on p.IdPregunta equals ra.IdPregunta into list1
                        from l1 in list1.DefaultIfEmpty()
                        join c in ctx.Clase on p.IdClase equals c.IdClase into list2
                        from l2 in list2.DefaultIfEmpty()
                        join re in ctx.ResultadoEvaluacion on l1.IdResultadoEvaluacion equals re.IdResultadoEvaluacion into list3
                        from l3 in list3.DefaultIfEmpty()
                        join t in ctx.Tema on p.IdTema equals t.IdTema into list4
                        from l4 in list4.DefaultIfEmpty()
                        select new ListarVerPreguntas
                        {
                            IdPregunta = p.IdPregunta,
                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            Clase = (l2 == null ? "NULL" :l2.Nombre),
                            Resultado = (l3 == null ? "NULL" : l3.Resultado),
                            Fecha = p.FechaDisponibleHasta,
                            Tema = (l4 == null ? "NULL" : l4.Nombre),
                            Orden = (l1==null ? 0 : l1.Orden),
                            Puntos = (l1==null ? 0 : l1.Puntos),
                            MejorRespuesta = (l1==null ? false : l1.MejorRespuesta)
                        };

            return query.ToList();

        }

        public static RespuestaAlumno verRespuesta(int idAlumno,int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno ra = ctx.RespuestaAlumno.Where(a => a.IdPregunta == idPregunta && a.IdAlumno == idAlumno).FirstOrDefault();
            return ra;
        }

        public static bool verFechaPreguntaValida(int idPregunta) {
            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta p = ctx.Pregunta.Where(a => a.IdPregunta == idPregunta).FirstOrDefault();
            if (p.FechaDisponibleHasta == null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public static bool verPreguntaValidaAlumno(int idAlumno,int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno p = ctx.RespuestaAlumno.Where(a => a.IdPregunta == idPregunta && a.IdAlumno == idAlumno).FirstOrDefault();
            if (p != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Pregunta obtenerPregunta(int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta p = ctx.Pregunta.Where(a => a.IdPregunta == idPregunta).FirstOrDefault();
            return p;
        }

        public static void guardarRespuesta(RespuestaAlumno ra, int idUsuario) {
            TP_20191CEntities ctx = new TP_20191CEntities();
            //Busco las preguntas con el mismo id para determinar el orden
            int orden = ctx.RespuestaAlumno.Where(o => o.IdPregunta == ra.IdPregunta).Count();
            orden = orden + 1;
            RespuestaAlumno nuevo = new RespuestaAlumno();
            nuevo.Orden = orden;
            nuevo.FechaHoraRespuesta = DateTime.Now;
            nuevo.IdAlumno = idUsuario;
            nuevo.IdPregunta = ra.IdPregunta;
            nuevo.Respuesta = ra.Respuesta;
            ctx.RespuestaAlumno.Add(nuevo);

            ctx.SaveChanges();

            //envio email a profesores
            List<Profesor> p = ctx.Profesor.ToList();
            foreach (Profesor profesor in p) {
                EmailServicio.enviarEmail(ra.IdPregunta, idUsuario, profesor.Email);
            }

        }

        public static bool plazoFecha(int idPregunta) {

            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta p = ctx.Pregunta.Where(a => a.IdPregunta == idPregunta).FirstOrDefault();

            DateTime fechaHasta = p.FechaDisponibleHasta??DateTime.Now;

            if (p.FechaDisponibleHasta == null)
            {
                return true;
            }
            else
            {
                int fecha = DateTime.Compare(fechaHasta, DateTime.Now);
                if (fecha <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
