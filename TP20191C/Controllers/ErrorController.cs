using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TP20191C.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 404:
                    ViewBag.Title = "Error 404";
                    ViewBag.Description = "Pagina Web no encontrada";
                    break;

                case 500:
                    ViewBag.Title = "Error 500";
                    ViewBag.Description = "Se esta teniendo problema con el servidor";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Disculpe las molestias ocacionadas";
                    break;
            }

            return View("~/Views/Error/_ErrorPage.cshtml");
            //return View();
        }
    }
}