using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyGame.DB.DB.Models;

namespace MyGame.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            using(var rep = new BuildingsRepository())
            {
                var TEST = rep.GetAll();
            }
            return View();
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