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
        private DTOMapper _DTOMapper;

        public CityPoiController(ICityRepository repository)
        {
            _repository = repository;
            _DTOMapper = new DTOMapper();
        }

        [HttpGet]
        public List<CityWithNoPOIDTO> GetAll()
        {
            var cityList = _repository.GetCities();
            var DTOList = cityList.Select(city => new CityWithNoPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            }).ToList();

            return DTOList;
        }

        [HttpGet("{id}", Name = "GetCity")]
        public IActionResult GetCity(int id, bool includePointsOfInterest)
        {
            var city = _repository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return new NotFoundResult();
            }

            if (includePointsOfInterest)
            {
                return new ObjectResult(new CityWithPOIDTO
                {
                    CityId = city.Id,
                    Name = city.Name,
                    Country = city.Country,
                    POIList = city.PointsOfInterest,
                    Population = city.Population
                });
            }
            else
            {
                return new ObjectResult(new CityWithNoPOIDTO
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