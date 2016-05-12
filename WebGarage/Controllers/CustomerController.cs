using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGarage.DAL;
using WebGarage.Models;

namespace WebGarage.Controllers
{
    public class CustomerController : Controller
    {

        private GarageContext db = new GarageContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Park Vehicle
        public ActionResult ParkVehicle()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ParkVehicle([Bind(Include = "ID,RegistrationNumber,NumberOfWheels,Model,Color,VehicleType,DateCreated")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        public ActionResult CheckoutVehicle()
        {
            return View();
        }

        [HttpPost, ActionName("CheckoutVehicle")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckoutVehicleConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
    }
}