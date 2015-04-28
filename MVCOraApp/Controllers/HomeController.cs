using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCOraApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "MVC Oracle CRUD";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aplicação MVC (EF5) integrada ao DB Oracle";

            var dataFile = Server.MapPath("~/App_Data/database_scripts.sql");
            var vData = System.IO.File.ReadAllText(dataFile);
            ViewBag.TextData = vData;

            return View();
        }
    }
}
