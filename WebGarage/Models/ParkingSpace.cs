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

        [Required]
        public bool Edge { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}