using System;
using CityPoiAPI.Controllers;
using CityPoiAPI.Entities;

namespace CityPoiAPI.DTO
{
    public class DTOMapper
    {
        public PointOfInterest PostPoiDtoToPoi(PostPOIDTO poiDTO)
        {
            return new PointOfInterest()
            {
                Address = poiDTO.Address,
                CityId = poiDTO.CityId,
                Description = poiDTO.Description,
                Latitude = poiDTO.Latitude,
                Longitude = poiDTO.Longitude,
                Name = poiDTO.Name
            };
        }

        public PostPOIDTO PoiToPostPoiDto(PointOfInterest poi)
        {
            return new PostPOIDTO()
            {
                Address = poi.Address,
                CityId = poi.CityId,
                Description = poi.Description,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude,
                Name = poi.Name
            };
        }

        public PointOfInterestDTO PoiToPoiDto(PointOfInterest poi)
        {
            return new PointOfInterestDTO()
            {
                Id = poi.Id,
                Address = poi.Address,
                CityId = poi.CityId,
                Description = poi.Description,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude,
                Name = poi.Name
            };
        }

        public PointOfInterest PoiDtoToPoi(PointOfInterestDTO poiDTO)
        {
            return new PointOfInterest()
            {
                Id = poiDTO.Id,
                Address = poiDTO.Address,
                CityId = poiDTO.CityId,
                Description = poiDTO.Description,
                Latitude = poiDTO.Latitude,
                Longitude = poiDTO.Longitude,
                Name = poiDTO.Name
            };
        }
    }
}
