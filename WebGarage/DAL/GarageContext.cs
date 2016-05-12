using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebGarage.Models;

namespace WebGarage.DAL
{
    public class GarageContext : DbContext
    {
        public GarageContext(): base ("WebGarage")
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }

    }
}