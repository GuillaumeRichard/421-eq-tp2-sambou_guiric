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
                Name = "Cegep Ste-Foy",
                Description = "Le cegep le plus cool",
                Address = "2700, ch. Ste-Foy",
                Longitude = "46.78589761",
                Latitude = "-71.28661394",
                ImageUrl = "http://www.cegep-ste-foy.qc.ca/typo3temp/_processed_/csm_ban_a_propos_01_9f03514825.jpg"
            };

            var poi2 = new PointOfInterest()
            {
                Name = "Château Frontenac",
                Description = "L\'icône de la ville de Québec",
                Address = "1, rue des Carrières",
                Longitude = "46.810756",
                Latitude = "-71.2044479",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d4/Chateau_Frontenac_Quebec_City.jpg"
            };

            var poi3 = new PointOfInterest()
            {
                Name = "Plaines d'Abraham",
                Description = "Lieu du Festival d'été de Québec",
                Address = "Avenue Wilfrid-Laurier",
                Longitude = "46.8015014",
                Latitude = "-71.2173963",
                ImageUrl = "http://www.ameriquefrancaise.org/media-3007/plaine_abraham_vue_aerienne.jpg"
            };

            apiDbContext.Cities.Add(new City()
            {
                Name = "Quebec",
                Country = "Canada",
                Population = 300000,
                PointsOfInterest = new List<PointOfInterest>()
                {
                    poi1,
                    poi2,
                    poi3                    
                }
            });

            apiDbContext.PointsOfInterest.Add(poi1);
            apiDbContext.PointsOfInterest.Add(poi2);
            apiDbContext.PointsOfInterest.Add(poi3);

            apiDbContext.SaveChanges();
        }
    }
}
