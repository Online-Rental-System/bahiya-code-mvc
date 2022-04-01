using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace car_rental_mvc.Models
{
    public class UpdateRent
    {
        
        public int Id { get; set; }
        public string CarBrand { get; set; }
       // public string Model { get; set; }
        //public string AvailabilityStatus { get; set; }
        public int? RentPrice { get; set; }
        //public string Carstatus { get; set; }
    }
}
