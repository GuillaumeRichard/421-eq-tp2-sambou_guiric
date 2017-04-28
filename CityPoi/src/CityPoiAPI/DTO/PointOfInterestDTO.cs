using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityPoiAPI.DTO
{
    public class PointOfInterestDTO
    {
        //YM: devrait contenir des validations + validation testées
        public int Id {get; set;}
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string Longitude { get; set; } 
        public string Latitude { get; set;}
    }
}
