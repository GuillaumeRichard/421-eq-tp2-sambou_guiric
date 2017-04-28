using System.Collections.Generic;
using System.Linq;
using CityPoiAPI.Entities;
using CityPoiAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CityPoiAPI.DataAccessLayer
{
    public class CityRepositoryEntityFramework : ICityRepository
    {
        private readonly ApiDbContext _context;

        public CityRepositoryEntityFramework(ApiDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool CityExists(string cityName)
        {
            var cities = _context.Cities.ToList();
            return cities.Any(element => element.Name == cityName);
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public City GetCity(string name, bool includePointsOfInterest)
        {
            return _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(string cityName)
        {
            var city = _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Name == cityName);
            return city.PointsOfInterest.ToList();
        }

        public PointOfInterest GetPointOfInterestForCity(string cityName, string pointOfInterestName)
        {
            var city = _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Name == cityName);
            return city.PointsOfInterest.FirstOrDefault(element => element.Name == pointOfInterestName);
        }

        public void AddPointOfInterestForCity(string cityName, PointOfInterest pointOfInterest)
        {
            _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Name == cityName).PointsOfInterest.Add(pointOfInterest);
            _context.SaveChanges();
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Name == pointOfInterest.Name).PointsOfInterest.Remove(pointOfInterest);
            _context.SaveChanges();
        }

        public void UpdatePointOfInterest(PointOfInterest newPointOfInterest)
        {
            var originalPoi = GetPointOfInterestForCity(newPointOfInterest.CityName, newPointOfInterest.Name);
            originalPoi = MapPoi(newPointOfInterest, originalPoi);
            _context.PointsOfInterest.Update(originalPoi);
            _context.SaveChanges();
        }

        private static PointOfInterest MapPoi(PointOfInterest newPointOfInterest, PointOfInterest originalPoi)
        {
            originalPoi.Address = newPointOfInterest.Address;
            originalPoi.Description = newPointOfInterest.Description;
            originalPoi.Latitude = newPointOfInterest.Latitude;
            originalPoi.Longitude = newPointOfInterest.Longitude;
            originalPoi.Name = newPointOfInterest.Name;
            return originalPoi;
        }
    }
}