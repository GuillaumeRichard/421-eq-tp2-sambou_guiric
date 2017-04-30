using System.Linq;
using CityPoiAPI.DTO;
using CityPoiAPI.Entities;
using CityPoiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPoiAPI.Controllers
{
    [Route("api/Cities")]
    public class PoiController:Controller
    {
        private readonly ICityRepository _repository;
        private readonly DTOMapper _dtoMapper;

        public PoiController(ICityRepository repository)
        {
            _repository = repository;
            _dtoMapper = new DTOMapper();
        }

        [HttpDelete("{cityId}/pointsofinterest/{poiId}", Name = "DeletePointOfInterest")]
        public IActionResult DeletePointOfIntetest(int cityId, int poiId)
        {
            if (!_repository.CityExists(cityId))
            {
                return new NotFoundResult();
            }

            var poi = _repository.GetPointOfInterestForCity(cityId, poiId);

            if (poi == null)
            {
                return NotFound();
            }
            else
            {
                _repository.DeletePointOfInterest(poi);
            }

            return NoContent();
        }

        //POUR VOIR SI GET POI LIST SUR LE CLIENT MARCHE
        //[HttpGet("{id}/pointsofinterest", Name = "GetPointsOfInterestForCity")]
        //public IActionResult GetPointsOfInterestForCity(int id)
        //{
        //    if (!_repository.CityExists(id))
        //    {
        //        return new NotFoundResult();
        //    }

        //    var cityPoIs = _repository.GetPointsOfInterestForCity(id);

        //    if (cityPoIs == null)
        //    {
        //        return new NotFoundResult(); 
        //    }


        //    return new ObjectResult(new PointsOfInterestDTO
        //    {
        //        POIList = cityPoIs.ToList()
        //    });
        //}

        [HttpGet("{name}/pointsofinterest", Name = "GetPointsOfInterestForCity")]
        public IActionResult GetPointsOfInterestForCity(string name)
        {
            var cities = _repository.GetCities();
            //a encapsuler
            City selectedCity = null;
            foreach (var city in cities)
            {
                if (city.Name.Equals(name))
                {
                    selectedCity = city;
                }
            }

            if (selectedCity == null)
            {
                return NotFound();
            }

            var cityPoIs = _repository.GetPointsOfInterestForCity(selectedCity.Id);

            if (cityPoIs == null)
            {
                return new NotFoundResult();
            }

            var objectResult = new ObjectResult(new PointsOfInterestDTO
            {
                POIList = cityPoIs.ToList()
            });

            return objectResult;
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

        [HttpPost]
        [Route("{cityId}/pointsofInterest", Name = "AddPointOfInterest")]
        public IActionResult AddPointOfInterestToCity(int cityId, [FromBody] PostPOIDTO poiDto)
        {
            if (!_repository.CityExists(cityId))
            {
                return new NotFoundResult();
            }
            if (poiDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poi = _dtoMapper.PostPoiDtoToPoi(poiDto);
            _repository.AddPointOfInterestForCity(cityId, poi);
            return CreatedAtRoute("AddPointOfInterest", new { id = poi.Id }, poi);
        }

        [HttpPut]
        [Route("{cityId}/pointsofInterest/{poiId}", Name = "PutPointOfInterest")]
        public IActionResult UpdatePointOfInterest(int poiId, [FromBody] PointOfInterestDTO poiDto)
        {
            if (poiDto == null || poiDto.Id != poiId)
            {
                return BadRequest();
            }

            var poi = _dtoMapper.PoiDtoToPoi(poiDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!ValidatePoiExists(poi))
            {
                return NotFound();
            }

            _repository.UpdatePointOfInterest(poi);
            return new NoContentResult();

        }

        private bool ValidatePoiExists(PointOfInterest poi)
        {
            const bool includePointsOfInterest = true;
            var city = _repository.GetCity(poi.CityId, includePointsOfInterest);
            return city.PointsOfInterest.Any(element => element.Id == poi.Id);
        }

    }
}
