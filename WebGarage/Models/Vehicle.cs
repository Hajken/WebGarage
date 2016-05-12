using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGarage.Models
{
    public class Vehicle
    {
        private DateTime _date = DateTime.Now;

        public int ID { get; set; }

        public string RegistrationNumber { get; set; }

        public int NumberOfWheels { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public VehicleTypes VehicleType { get; set; }

        public DateTime DateCreated
        {
            get { return _date; }
            set { _date = value; }
        }



    }

    public enum VehicleTypes
    {
        Car,
        Truck,
        Boat,
        AirPlane,
        Bicycle,
        Motorcycle
    }
}