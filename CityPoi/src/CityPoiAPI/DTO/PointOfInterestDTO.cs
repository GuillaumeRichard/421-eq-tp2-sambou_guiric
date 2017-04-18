﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityPoiAPI.DTO
{
    public class PointOfInterestDTO
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set;}
    }
}
