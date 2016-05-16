using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGarage.DAL;

namespace WebGarage.Controllers
{
    public class HomeController : Controller
    {
        private GarageContext db = new GarageContext();
        public ActionResult Index()
        {
            db.Vehicles.ToList();
            ViewBag.FreeParkingSlots = db.Vehicles.ToList().Count().ToString();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Administration";

            return View();
        }
    }
}