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
        private readonly DtoMapper _dtoMapper;

        public PoiController(ICityRepository repository)
        {
            _repository = repository;
            _dtoMapper = new DtoMapper();
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

        [HttpGet("{id}/pointsofinterest", Name = "GetPointsOfInterestForCity")]
        public IActionResult GetPointsOfInterestForCity(int id)
        {
            if (!_repository.CityExists(id))
            {
                return new NotFoundResult();
            }

            var cityPoIs = _repository.GetPointsOfInterestForCity(id);

            if (cityPoIs == null)
            {
                return new NotFoundResult();
            }


            return new ObjectResult(new PointsOfInterestDto
            {
                PoiList = cityPoIs.ToList()
            });
        }

        //[HttpGet("{name}/pointsofinterest", Name = "GetPointsOfInterestForCity")]
        //public IActionResult GetPointsOfInterestForCity(string name)
        //{
        //    var cities = _repository.GetCities();
        //    //a encapsuler
        //    City selectedCity = null;
        //    foreach (var city in cities)
        //    {
        //        if (city.Name.Equals(name))
        //        {
        //            selectedCity = city;
        //        }
        //    }

        //    if (selectedCity == null)
        //    {
        //        return NotFound();
        //    }

        //    var cityPoIs = _repository.GetPointsOfInterestForCity(selectedCity.Id);

        //    if (cityPoIs == null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    var objectResult = new ObjectResult(new PointsOfInterestDTO
        //    {
        //        POIList = cityPoIs.ToList()
        //    });

        //    return objectResult;
        //}

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

            return new ObjectResult(_dtoMapper.PoiToPoiDto(poi));
        }

        [HttpPut]
        [Route("{cityId}/pointsofInterest/{poiId}", Name = "PutPointOfInterest")]
        public IActionResult UpdatePointOfInterest(int poiId, [FromBody] PointOfInterestDto poiDto)
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
