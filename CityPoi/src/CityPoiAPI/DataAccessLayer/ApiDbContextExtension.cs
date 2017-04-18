using System;
using System.Collections.Generic;
using System.Linq;
using CityPoiAPI.Entities;

namespace CityPoiAPI.DataAccessLayer
{
    public static class ApiDbContextExtension
    {
        public static void EnsureSeedDataForContext(this ApiDbContext apiDbContext)
        {
            if (apiDbContext.Cities.Any())
            {
                return;
            }

            var poi1 = new PointOfInterest()
            {
                Address = "1515 av. Smith",
                Description = "A rock that bounces",
                Name = "Bouncing rock",
                Longitude = "100.2421",
                Latitude = "25.2421"
            };

            var poi2 = new PointOfInterest()
            {
                Address = "1515 av. Johnson",
                Description = "A rock that is named Angelo",
                Name = "Angelo rock",
                Longitude = "100.2321",
                Latitude = "25.2321"
            };

            var poi3 = new PointOfInterest()
            {
                Address = "1515 av. Ghetto",
                Description = "A neighbourhood that is bad",
                Name = "Bad neighbourhood",
                Longitude = "83.2021",
                Latitude = "42.1921"
            };

            var poi4 = new PointOfInterest()
            {
                Address = "1515 av. Hood",
                Description = "A rock with a face",
                Name = "Face rock",
                Longitude = "82.2021",
                Latitude = "42.2021"
            };

            apiDbContext.Cities.Add(new City()
            {
                Name = "Morioh",
                Country = "Japan",
                Population = 3000,
                PointsOfInterest = new List<PointOfInterest>()
                {
                    poi1,
                    poi2                    
                }
            });
            apiDbContext.Cities.Add(new City()
            {
                Name = "Detroit",
                Country = "USA",
                Population = 3000,
                PointsOfInterest = new List<PointOfInterest>()
                {
                    poi3,
                    poi4                    
                }
            });

            apiDbContext.PointsOfInterest.Add(poi1);
            apiDbContext.PointsOfInterest.Add(poi2);
            apiDbContext.PointsOfInterest.Add(poi3);
            apiDbContext.PointsOfInterest.Add(poi4);

            apiDbContext.SaveChanges();
        }
    }
}
