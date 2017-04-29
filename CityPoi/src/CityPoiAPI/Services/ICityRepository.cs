using System.Collections.Generic;
using CityPoiAPI.Entities;

namespace CityPoiAPI.Services
{
    public interface ICityRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities();
        City GetCity(string name, bool includePointsOfInterest);
<<<<<<< HEAD
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(string cityName);
        PointOfInterest GetPointOfInterestForCity(string cityName, string pointOfInterestName);
=======
        City GetCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
>>>>>>> parent of 0516725... repository: recherche par id => recherche par nom
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        void UpdatePointOfInterest(PointOfInterest pointOfInterest);
    }
}
