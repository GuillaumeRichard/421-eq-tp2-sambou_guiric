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

        [HttpDelete("{cityId}/pointsofinterest/{poiId}", Name = "DeletePointOfInterest")]
        public IActionResult DeletePointOfIntetest(int CityId, int PoiId)
        {
            if (!_repository.CityExists(CityId))
            {
                return new NotFoundResult();
            }

            var city = _repository.GetCity(CityId, true);

            foreach (var element in city.PointsOfInterest)
            {
                if (element.Id == PoiId)
                {
                    _repository.DeletePointOfInterest(element);
                    return new NoContentResult();
                }
            }
            return new NotFoundResult();
        }

        [HttpGet("{id}/pointsofinterest", Name = "GetPointsOfInterestForCity")]
        public IActionResult GetPointsOfInterestForCity(int id)
        {
            if (!_repository.CityExists(id))
            {
                return new NotFoundResult();
            }

            var cityPOIs = _repository.GetPointsOfInterestForCity(id);

            if (cityPOIs == null)
            {
                return new NotFoundResult();
            }


            return new ObjectResult(new PointsOfInterestDTO
            {
                POIList = cityPOIs.ToList()
            });
        }

        [HttpGet("{cityId}/pointsofinterest/{poiId}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int poiId)
        {
            if (!_repository.CityExists(cityId))
            {
                return new NotFoundResult();
            }

            var poi = _repository.GetPointOfInterestForCity(cityId, poiId);

            if (poi == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(new PointOfInterestDTO
            {
                Id = poi.Id,
                Name = poi.Name,
                Address = poi.Address,
                Description = poi.Description,
                CityId = poi.CityId,
                Longitude = poi.Longitude,
                Latitude = poi.Latitude
            });
        }

        [HttpGet("{id}", Name = "GetCity")]
        public IActionResult GetCity(int Id, bool includePointsOfInterest)
        {
            var city = _repository.GetCity(Id, includePointsOfInterest);

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

        [HttpPost]
        [Route("{cityId}/pointsofInterest", Name = "AddPointOfInterest")]
        public IActionResult AddPointOfInterestToCity(int cityId, [FromBody] PostPOIDTO poiDTO)
        {
            if (!_repository.CityExists(cityId))
            {
                return new NotFoundResult();
            }
            if (poiDTO == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poi = _DTOMapper.PostPoiDtoToPoi(poiDTO);
            _repository.AddPointOfInterestForCity(cityId, poi);
            return CreatedAtRoute("AddPointOfInterest", new { id = poi.Id }, poi);
        }

        [HttpPut]
        [Route("{cityId}/pointsofInterest/{poiId}", Name = "PutPointOfInterest")]
        public IActionResult UpdatePointOfInterest(int poiId, [FromBody] PointOfInterestDTO poiDTO)
        {
            if (poiDTO == null || poiDTO.Id != poiId)
            {
                return BadRequest();
            }

            var poi = _DTOMapper.PoiDtoToPoi(poiDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!validatePoiExists(poi))
            {
                return NotFound();
            }

            _repository.UpdatePointOfInterest(poi);
            return new NoContentResult();
                
        }

        private bool validatePoiExists(PointOfInterest poi)
        {
            bool includePointsOfInterest = true;
            var city = _repository.GetCity(poi.CityId, includePointsOfInterest);
            foreach (var element in city.PointsOfInterest)
            {
                if (element.Id == poi.Id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}