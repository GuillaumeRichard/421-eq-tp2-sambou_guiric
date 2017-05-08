using System;
using System.Collections.Generic;
using System.Linq;
using CityPoiAPI.DTO;
using CityPoiAPI.Entities;
using CityPoiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPoiAPI.Controllers
{
    [Route("api/Cities")]
    public class CityPoiController : Controller
    {
        private ICityRepository _repository;
        private DtoMapper _dtoMapper;

        public CityPoiController(ICityRepository repository)
        {
            _repository = repository;
            _dtoMapper = new DtoMapper();
        }

        [HttpGet]
        public List<CityWithNoPoidto> GetAll()
        {
            var cityList = _repository.GetCities();
            var dtoList = cityList.Select(city => new CityWithNoPoidto
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            }).ToList();

            return dtoList;
        }

        [HttpGet("{id}", Name = "GetCityById")]
        public IActionResult GetCity(int id, bool includePointsOfInterest)
        {
            var city = _repository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return new NotFoundResult();
            }

            if (includePointsOfInterest)
            {
                return new ObjectResult(new CityWithPoidto
                {
                    CityId = city.Id,
                    Name = city.Name,
                    Country = city.Country,
                    PoiList = city.PointsOfInterest,
                    Population = city.Population
                });
            }
            else
            {
                return new ObjectResult(new CityWithNoPoidto
                {
                    CityId = city.Id,
                    Name = city.Name,
                    Country = city.Country,
                    Population = city.Population
                });
            }
        }

        [HttpGet("{Name}", Name = "GetCityByName")]
        public IActionResult GetCity(string Name, bool includePointsOfInterest)
        {
            var city = _repository.GetCity(Name, includePointsOfInterest);

            if (city == null)
            {
                return new NotFoundResult();
            }

            if (includePointsOfInterest)
            {
                return new ObjectResult(new CityWithPoidto
                {
                    CityId = city.Id,
                    Name = city.Name,
                    Country = city.Country,
                    PoiList = city.PointsOfInterest,
                    Population = city.Population
                });
            }
            else
            {
                return new ObjectResult(new CityWithNoPoidto
                {
                    CityId = city.Id,
                    Name = city.Name,
                    Country = city.Country,
                    Population = city.Population
                });
            }
        }
    }
}