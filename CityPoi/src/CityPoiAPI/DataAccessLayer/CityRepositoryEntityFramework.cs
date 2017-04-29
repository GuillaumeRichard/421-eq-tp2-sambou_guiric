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

        public bool CityExists(int cityId)
        {
            var cities = _context.Cities.ToList();
            return cities.Any(element => element.Id == cityId);
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public City GetCity(string name, bool includePointsOfInterest)
        {
            return _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Name == name);
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            return _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Id == cityId);
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            var city = _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Id == cityId);
            return city.PointsOfInterest.ToList();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            var city = _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Id == cityId);
            return city.PointsOfInterest.FirstOrDefault(element => element.Id == pointOfInterestId);
        }

<<<<<<< HEAD
=======
        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Id == cityId).PointsOfInterest.Add(pointOfInterest);
            _context.SaveChanges();
        }

>>>>>>> parent of 0516725... repository: recherche par id => recherche par nom
        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(x => x.Id == pointOfInterest.CityId).PointsOfInterest.Remove(pointOfInterest);
            _context.SaveChanges();
        }

        public void UpdatePointOfInterest(PointOfInterest newPointOfInterest)
        {
            var originalPoi = GetPointOfInterestForCity(newPointOfInterest.CityId, newPointOfInterest.Id);
            originalPoi = MapPoi(newPointOfInterest, originalPoi); // YM: non nécessaire 
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