using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEGASY.Encripta;

namespace LEGASY.Controllers
{
    
    public class HomeController : PrincipalController
    {

        public ActionResult Index()
        {

            if (ValidaSesion())
            {
                ViewBag.Activo = "active";
                return View();
            }
            else
            {
                return Salir();
            }

           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}