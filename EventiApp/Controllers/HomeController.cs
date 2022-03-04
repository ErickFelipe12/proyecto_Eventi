using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EventiApp.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descripción Eventi";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto con nosostros";

            return View();
        }
    }
}