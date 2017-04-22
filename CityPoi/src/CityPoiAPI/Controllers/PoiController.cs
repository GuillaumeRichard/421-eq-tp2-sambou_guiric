using System.Linq;
using CityPoiAPI.DTO;
using CityPoiAPI.Entities;
using CityPoiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPoiAPI.Controllers
{
    [Route("api/Cities/{cityId}/pointsofinterest")]
    public class PoiController:Controller
    {
        private readonly ICityRepository _repository;
        private readonly DTOMapper _dtoMapper;

        public PoiController(ICityRepository repository)
        {
            _repository = repository;
            _dtoMapper = new DTOMapper();
        }

        [HttpDelete("{poiId}", Name = "DeletePointOfInterest")]
        public IActionResult DeletePointOfIntetest(int cityId, int poiId)
        {
            if (!_repository.CityExists(cityId))
            {
                return new NotFoundResult();
            }

            var city = _repository.GetCity(cityId, true);

            foreach (var element in city.PointsOfInterest)  //YM: Pourquoi un foreach ?? Appeler le delete du repo.
            {
                if (element.Id != poiId) continue;
                _repository.DeletePointOfInterest(element);
                return new NoContentResult();
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public IActionResult GetPointsOfInterestForCity(int id)
        {
            if (!_repository.CityExists(id))
            {
                return new NotFoundResult();
            }

            var cityPoIs = _repository.GetPointsOfInterestForCity(id);

            if (cityPoIs == null)
            {
                return new NotFoundResult();  //YM: non couvert par les tests 
            }


            return new ObjectResult(new PointsOfInterestDTO
            {
                POIList = cityPoIs.ToList()
            });
        }

        [HttpGet("{poiId}", Name = "GetPointOfInterest")]
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

            return new ObjectResult(new PointOfInterestDTO  //YM: utiliser un mapper 
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
        [Route("{poiId}", Name = "PutPointOfInterest")]
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
