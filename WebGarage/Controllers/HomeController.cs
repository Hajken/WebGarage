﻿using System;
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
        private CustomerController customercontroller = new CustomerController();
        
        public ActionResult Index()
        {
            return View(db.ParkingSpaces.ToList());
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