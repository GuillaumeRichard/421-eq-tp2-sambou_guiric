using System.Collections.Generic;
using CityPoiAPI.Entities;

namespace CityPoiAPI.Services
{
    public interface ICityRepository
    {
        bool CityExists(string cityName);
        IEnumerable<City> GetCities();
        City GetCity(string name, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(string cityName);
        PointOfInterest GetPointOfInterestForCity(string cityName, string pointOfInterestName);
        void AddPointOfInterestForCity(string cityName, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        void UpdatePointOfInterest(PointOfInterest pointOfInterest);
    }
}
