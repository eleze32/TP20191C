using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicios
{
    public class AlumnoServicio
    {
        /* retorna todos los alumnos ordenados de forma decendente por puntos totales, despues por cantidad de repuestas
         * correctas,y despues por cantidad de mejor repuestas todas de forma descendente 
         */
        public static List<Alumno> TablaPosiciones()
        {
            TP_20191CEntities ctx = new TP_20191CEntities();

            /* se hace un select donde se pide que traiga todos los alumnos pero con algunas columnas, no todas
             Se crea objeto de tipo anonimo para guardar las columna necesarias. Despues los ordeno. Se crea la lista
             ya ordenada pero con tipo anonimo. Despues se vuelve a hacer un select para crear objetos alumnos y a partir
             de ahi crea la lista de alumnos.*/
            List <Alumno>  alumnos = ctx.Alumno.OrderByDescending(x => x.PuntosTotales).
                ThenByDescending(x => x.CantidadRespuestasCorrectas).ThenByDescending(x => x.CantidadMejorRespuesta).
                ToList();

            return alumnos;
        }
    }
}
