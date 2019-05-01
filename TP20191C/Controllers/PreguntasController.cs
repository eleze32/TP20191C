using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP20191C.Controllers
{
    public class PreguntasController : Controller
    {
        // GET: Preguntas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Administrar()
        {
            return View();
        }

        public ActionResult Modificar()
        {
            return View();
        }

        public ActionResult Eliminar()
        {
            return View();
        }
    }
}