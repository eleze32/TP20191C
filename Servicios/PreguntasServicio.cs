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
    public class PreguntasServicio
    {
        // Obtiene las ultimas dos preguntas donde la fechadisponiblehasta es menir a la fechahora actual
        
        public static List<Pregunta> UltimasDospreguntas()
        {
            DateTime fecha = DateTime.Now;
            TP_20191CEntities ctx = new TP_20191CEntities();

            //dos ultimas preguntas menores a la fecha dada, ordenada por numero de pregunta de forma descendente
            List<Pregunta> ultimas_dos_preguntas = ctx.Pregunta.Where(x => x.FechaDisponibleHasta < fecha).
                OrderByDescending(x => x.Nro).Take(2).ToList();

            List<Pregunta> preguntas_ordenadas = new List<Pregunta>();
            
            //ordenas las repuestas de los alumnos por puntos 
            foreach(Pregunta p in ultimas_dos_preguntas)
            {
                List<RespuestaAlumno> ordenado_por_puntos = p.RespuestaAlumno.Where(x => x.Puntos > 0).OrderByDescending(x => x.Puntos).ToList();
                p.RespuestaAlumno = ordenado_por_puntos;
                preguntas_ordenadas.Add(p);
            }

            return preguntas_ordenadas;
        }

        public static List<Pregunta> ObtenerTodasLasPreguntas()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            List < Pregunta > preguntas = ctx.Pregunta.ToList();
            return preguntas;
        }
       
        public static bool ExisteNumeroDePregunta(int nro_pregunta,TP_20191CEntities ctx)
        {
            Pregunta p = ctx.Pregunta.Where(x => x.Nro == nro_pregunta).FirstOrDefault();

            if (p == null)
                return false;

            return true;
        }

        public static Pregunta ObtenerPreguntaporId(int IdPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            Pregunta pregunta = ctx.Pregunta.Where(x => x.IdPregunta == IdPregunta).FirstOrDefault();
            return pregunta;
        }

        public static int ObtenerNumeroProximaPregunta()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            int proxina_pregunta = ctx.Pregunta.OrderByDescending(x => x.Nro).Take(1).First().Nro + 1;
            return proxina_pregunta;
        }
        
        public static List<PreguntaRespuestaAlumno> preguntasSinResponder(int id)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join pr in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id) on p.IdPregunta equals pr.IdPregunta into gj
                        from x in gj.DefaultIfEmpty()
                        select new PreguntaRespuestaAlumno
                        {

                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            IdAlumno = (x == null ? 0 : x.IdAlumno),
                            IdPregunta = p.IdPregunta
                        };

            return query.Where(a => a.IdAlumno == 0).ToList();

        }

        private static List<ListarVerPreguntas> preguntasMal(int id)
        {

            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id && a.IdResultadoEvaluacion == 3) on p.IdPregunta equals ra.IdPregunta
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

        private static List<ListarVerPreguntas> preguntasCorrectas(int id)
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
                            InicioFecha = p.FechaDisponibleDesde,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();

        }

        private static List<ListarVerPreguntas> preguntasRegular(int id)
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
                            InicioFecha = p.FechaDisponibleDesde,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();
        }

        private static List<ListarVerPreguntas> preguntasSinCorregir(int id)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            var query = from p in ctx.Pregunta
                        join ra in ctx.RespuestaAlumno.Where(a => a.IdAlumno == id && a.IdResultadoEvaluacion == null) on p.IdPregunta equals ra.IdPregunta
                        join c in ctx.Clase on p.IdClase equals c.IdClase              
                        join t in ctx.Tema on p.IdTema equals t.IdTema
                        select new ListarVerPreguntas
                        {
                            IdPregunta = p.IdPregunta,
                            Nro = p.Nro,
                            Pregunta = p.Pregunta1,
                            Clase = c.Nombre,
                            Resultado = (ra.IdResultadoEvaluacion == null ? "NULL" : "NULL"),
                            InicioFecha = p.FechaDisponibleDesde,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = t.Nombre,
                            Orden = ra.Orden,
                            Puntos = ra.Puntos,
                            MejorRespuesta = ra.MejorRespuesta
                        };
            return query.OrderByDescending(a => a.Nro).ToList();

        }

        private static List<ListarVerPreguntas> preguntasTodas(int id)
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
                            Clase = (l2 == null ? "NULL" : l2.Nombre),
                            Resultado = (l3 == null ? "NULL" : l3.Resultado),
                            InicioFecha = p.FechaDisponibleDesde,
                            Fecha = p.FechaDisponibleHasta,
                            Tema = (l4 == null ? "NULL" : l4.Nombre),
                            Orden = (l1 == null ? 0 : l1.Orden),
                            Puntos = (l1 == null ? 0 : l1.Puntos),
                            MejorRespuesta = (l1 == null ? false : l1.MejorRespuesta)
                        };

            return query.OrderByDescending(a => a.Nro).ToList();
        }
        
        public static bool verPreguntaValidaAlumno(int idAlumno, int idPregunta)
        {
            TP_20191CEntities ctx = new TP_20191CEntities();
            RespuestaAlumno p = ctx.RespuestaAlumno.Where(a => a.IdPregunta == idPregunta && a.IdAlumno == idAlumno).FirstOrDefault();
            if (p != null)
                 return true;
            
            return false;
        }
        
        // retorna 1 cuando fecha es menor o igual a 0 y retonar 0 si es mayor a 0, retorna -1 cuando cualquiera de las fechas es null
        public static int VerifcaPlazoFecha(Pregunta p)
        {
            DateTime fechaHasta = p.FechaDisponibleHasta ?? DateTime.Now;
            int fecha = DateTime.Compare(fechaHasta, DateTime.Now);

            if (p.FechaDisponibleHasta == null || p.FechaDisponibleDesde == null)
                return -1;  
            
            if (fecha <= 0)
                return 1;
             else
                return 0;
        }

        public static List<ListarVerPreguntas> ObtenerPreguntasPorFiltro(int id_usuario, int filtro)
        {
            List<ListarVerPreguntas> preguntas = new List<ListarVerPreguntas>();

            switch (filtro)
            {
                case -1:
                    preguntas = preguntasTodas(id_usuario);
                    break;
                case 0:
                    preguntas = preguntasSinCorregir(id_usuario);
                    break;
                case 1:
                    preguntas = preguntasCorrectas(id_usuario);
                    break;
                case 2:
                    preguntas = preguntasRegular(id_usuario);
                    break;
                case 3:
                    preguntas = preguntasMal(id_usuario);
                    break;
            }

            return preguntas;
        }
    }
}
