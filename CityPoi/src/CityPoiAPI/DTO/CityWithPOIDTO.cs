﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityPoiAPI.Entities;

namespace CityPoiAPI.DTO
{
    public class CityWithPoidto
    { 
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public IEnumerable<PointOfInterest> PoiList { get; set; }
    }
}
