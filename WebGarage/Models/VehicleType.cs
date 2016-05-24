using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebGarage.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class VehicleType
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Size { get; set; }

        public string Description { get; set; }
    }
}