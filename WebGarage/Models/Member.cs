using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebGarage.Models
{
    public class Member 
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PersonNumber { get; set; }

        [Required]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}