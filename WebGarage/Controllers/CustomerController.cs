using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebGarage.DAL;
using WebGarage.Models;

namespace WebGarage.Controllers
{
    public class CustomerController : Controller
    {
        const int pricePerMinut = 1;
        const int totalParkingSpaces = 200;
        private GarageContext db = new GarageContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Park Vehicle
        public ActionResult ParkVehicle()
        {
            if (FreeParkingSpaces() <= totalParkingSpaces)
            {
                ViewBag.GarageFull = null;
                return View();
            }


            ViewBag.GarageFull = "The Garage Is FULL";
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ParkVehicle([Bind(Include = "ID,RegistrationNumber,NumberOfWheels,Model,Color,VehicleType,DateCreated")] Vehicle vehicle)
        {
            if (FreeParkingSpaces() <= totalParkingSpaces)
            {

                if (ModelState.IsValid)
                {
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.GarageFull = "The Garage Is FULL";
            }
            return View(vehicle);
        }


        public ActionResult CheckoutVehicle(string searchTerm = null)
        {


            if (Request.IsAjaxRequest())
            {

                var vehicle = (from u in db.Vehicles
                               where u.RegistrationNumber == searchTerm
                               select u).FirstOrDefault();
                if (vehicle == null)
                {
                    ViewBag.Message = "Didnt find your vehicle!";
                }
                return PartialView("_CheckoutResult", vehicle);

            }

            return View();
        }

        public ActionResult CheckoutVehicleConfirmed(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id == null)
                {
                    ViewBag.Fail = "Did not find your vehicle";
                    return PartialView("_CheckoutResult", null);
                }
                Vehicle vehicle = db.Vehicles.Find(id);

                if (vehicle == null)
                {
                    ViewBag.Fail = "Did not find your vehicle";
                    return PartialView("_CheckoutResult", null);

                }
                GetReceipt(vehicle);

                db.Entry(vehicle).State = EntityState.Deleted;

                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        var entry = ex.Entries.Single();
                        //The MSDN examples use Single so I think there will be only one
                        //but if you prefer - do it for all entries
                        //foreach(var entry in ex.Entries)
                        //{
                        if (entry.State == EntityState.Deleted)
                            //When EF deletes an item its state is set to Detached
                            //http://msdn.microsoft.com/en-us/data/jj592676.aspx
                            entry.State = EntityState.Detached;
                        else
                            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                        //throw; //You may prefer not to resolve when updating
                        //}
                    }
                } while (saveFailed);

                return PartialView("_Receipt", null);
            }
            ViewBag.Fail = "Did not find your vehicle";
            return PartialView("_CheckoutResult", null);

        }

        public void GetReceipt(Vehicle vehicle)
        {
            int timeInMinuts = 0;
            int totalPrice = 0;

            DateTime StartTime = vehicle.DateCreated;
            DateTime EndTime = DateTime.Now;
            TimeSpan ts = EndTime - StartTime;


            timeInMinuts = (int)ts.TotalMinutes;
            totalPrice = timeInMinuts * pricePerMinut; //pricePerMinut is Const int and is price per minut


            ViewBag.TotalPrice = totalPrice.ToString();
            ViewBag.TotalTime = timeInMinuts.ToString();
            ViewBag.RegistrationNumber = vehicle.RegistrationNumber;
            ViewBag.VehicleId = vehicle.ID.ToString();
            ViewBag.VehicleModel = vehicle.Model;
            ViewBag.VehicleType = vehicle.VehicleType;
            ViewBag.ParkingTime = vehicle.DateCreated.ToString();
            ViewBag.ParkingEnd = EndTime.ToString();
            ViewBag.DateNow = DateTime.Now.ToString();
            ViewBag.PricePerMinut = pricePerMinut.ToString();

        }

        public int FreeParkingSpaces()
        {
            int bike = 0;
            int freeParkingSlot = 0;

            var vehicles = db.Vehicles.ToList();


            foreach (var v in vehicles)
            {
                switch (v.VehicleType)
                {
                    case VehicleTypes.Car:
                        freeParkingSlot++;
                        break;
                    case VehicleTypes.Truck:
                        freeParkingSlot += 2;
                        break;
                    case VehicleTypes.Boat:
                        freeParkingSlot++;
                        break;
                    case VehicleTypes.AirPlane:
                        freeParkingSlot += 2;
                        break;
                    case VehicleTypes.Bicycle:
                        if (bike == 0)
                        {
                            freeParkingSlot++;
                            bike++;
                            break;
                        }
                        bike++;
                        if (bike == 3)
                        {
                            bike = 0;
                        }
                        break;
                    case VehicleTypes.Motorcycle:
                        if (bike == 0)
                        {
                            freeParkingSlot++;
                            bike++;
                            break;
                        }
                        bike++;
                        if (bike == 3)
                        {
                            bike = 0;
                        }
                        break;
                    case VehicleTypes.Other:
                        freeParkingSlot++;
                        break;
                    default:
                        break;
                }
            }
            return freeParkingSlot= totalParkingSpaces-freeParkingSlot;
        }
    }
}