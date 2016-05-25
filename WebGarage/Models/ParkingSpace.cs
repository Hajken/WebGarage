using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebGarage.Models
{
    public class ParkingSpace 
    {
        public int ID { get; set; }

        public int Lot { get; set; }

        public bool Edge { get; set; }

        public int? VehicleID { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}