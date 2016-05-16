using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebGarage.DAL;
using WebGarage.Models;
using WebGarage.Classes;
using PagedList;
    
namespace WebGarage.Controllers
{
    public class VehiclesController : Controller
    {
        private GarageContext db = new GarageContext();
        const int pricePerMinut = 1;
        // GET: Vehicles
        public ActionResult Index()
        {
            return View(db.Vehicles.ToList());
        }

        [HttpGet]
        public ActionResult SearchForm()
        {
            return PartialView("_SearchFormPartial");
        }


        [HttpGet]
        public ActionResult SearchInitial()
        {
            return SearchGet(null, null, null, null);
        }

        [HttpGet]
        public ActionResult SearchGet(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RegnrSortParm = String.IsNullOrEmpty(sortOrder) ? "regnr_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var vehicles = from v in db.Vehicles
                           select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(v => v.RegistrationNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "regnr_desc":
                    vehicles = vehicles.OrderByDescending(v => v.RegistrationNumber);
                    break;
                case "Date":
                    vehicles = vehicles.OrderBy(v => v.DateCreated);
                    break;
                case "date_desc":
                    vehicles = vehicles.OrderByDescending(v => v.DateCreated);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.RegistrationNumber);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var model = vehicles.ToPagedList(pageNumber, pageSize);

            return PartialView("_SearchResultsPartial", model);
        }


        [HttpPost]
        public ActionResult SearchPost(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RegnrSortParm = String.IsNullOrEmpty(sortOrder) ? "regnr_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var vehicles = from v in db.Vehicles
                           select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(v => v.RegistrationNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "regnr_desc":
                    vehicles = vehicles.OrderByDescending(v => v.RegistrationNumber);
                    break;
                case "Date":
                    vehicles = vehicles.OrderBy(v => v.DateCreated);
                    break;
                case "date_desc":
                    vehicles = vehicles.OrderByDescending(v => v.DateCreated);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.RegistrationNumber);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var model = vehicles.ToPagedList(pageNumber, pageSize);

            return PartialView("_SearchResultsPartial", model);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Vehicles/Stats
        public ActionResult Stats()
        {
            DateTime EndTime = DateTime.Now;

            var stats = new Stats();

            var vehicles = db.Vehicles.ToList();

            foreach (var v in vehicles)
            {
                switch (v.VehicleType)
                {
                    case VehicleTypes.Car:
                        stats.Car++;
                        break;
                    case VehicleTypes.Truck:
                        stats.Truck++;
                        break;
                    case VehicleTypes.Boat:
                        stats.Boat++;
                        break;
                    case VehicleTypes.AirPlane:
                        stats.AirPlane++;
                        break;
                    case VehicleTypes.Bicycle:
                        stats.Bicycle++;
                        break;
                    case VehicleTypes.Motorcycle:
                        stats.Motorcycle++;
                        break;
                    case VehicleTypes.Other:
                        stats.Other++;
                        break;
                    default:
                        break;
                }

                DateTime StartTime = v.DateCreated;
                
                TimeSpan ts = EndTime - StartTime;


                stats.TotalTimeInMinutes += (int)ts.TotalMinutes;
                stats.TotalNumberOfWheels += v.NumberOfWheels;

                stats.Total++;
            }
            stats.TotalPrice = stats.TotalTimeInMinutes * pricePerMinut;
            return View(stats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
