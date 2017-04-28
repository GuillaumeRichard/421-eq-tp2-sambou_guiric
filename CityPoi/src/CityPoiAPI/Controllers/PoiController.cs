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
        public IActionResult DeletePointOfIntetest(string cityId, string poiId)
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

        [HttpGet("{name}/pointsofinterest", Name = "GetPointsOfInterestForCity")]
        public IActionResult GetPointsOfInterestForCity(string name)
        {
            if (!_repository.CityExists(name))
            {
                return new NotFoundResult();
            }

            var cityPoIs = _repository.GetPointsOfInterestForCity(name);

            if (cityPoIs == null)
            {
                return new NotFoundResult(); 
            }


            return new ObjectResult(new PointsOfInterestDTO
            {
                POIList = cityPoIs.ToList()
            });
        }

        [HttpGet("{cityId}/pointsofinterest/{poiId}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(string cityId, string poiId)
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
                CityName = poi.CityName,
                Longitude = poi.Longitude,
                Latitude = poi.Latitude
            });
        }

        [HttpPut]
        [Route("{cityId}/pointsofInterest/{poiId}", Name = "PutPointOfInterest")]
        public IActionResult UpdatePointOfInterest(string poiId, [FromBody] PointOfInterestDTO poiDto)
        {
            if (poiDto == null || poiDto.Name != poiId)
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
            var city = _repository.GetCity(poi.CityName, includePointsOfInterest);
            return city.PointsOfInterest.Any(element => element.Name == poi.Name);
        }

    }
}
