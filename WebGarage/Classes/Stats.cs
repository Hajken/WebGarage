using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGarage.Classes
{
    public class Stats
    {
        public int Car { get; set; }
        public int Truck { get; set; }
        public int Boat { get; set; }
        public int AirPlane { get; set; }
        public int Bicycle { get; set; }
        public int Motorcycle { get; set; }
        public int Other { get; set; }

        public int Total { get; set; }
    }
}