using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebGarage.Models
{
    public class Vehicle
    {
        private DateTime _date = DateTime.Now;

        public int ID { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public int NumberOfWheels { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public Colors Color { get; set; }

        [Required]
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
        Motorcycle,
        Other
    }
    public enum Colors
    {
        Yellow,
        Black,
        White,
        Red,
        Green,
        Other
    }
    
}