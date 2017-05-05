using System;
using CityPoiAPI.Controllers;
using CityPoiAPI.Entities;

namespace CityPoiAPI.DTO
{
    public class DtoMapper
    {

        public PointOfInterestDto PoiToPoiDto(PointOfInterest poi)
        {
            return new PointOfInterestDto()
            {
                Id = poi.Id,
                Address = poi.Address,
                CityId = poi.CityId,
                Description = poi.Description,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude,
                Name = poi.Name,
                ImageUrl = poi.ImageUrl
            };
        }

        public PointOfInterest PoiDtoToPoi(PointOfInterestDto poiDto)
        {
            return new PointOfInterest()
            {
                Id = poiDto.Id,
                Address = poiDto.Address,
                CityId = poiDto.CityId,
                Description = poiDto.Description,
                Latitude = poiDto.Latitude,
                Longitude = poiDto.Longitude,
                Name = poiDto.Name,
                ImageUrl = poiDto.ImageUrl
            };
        }
    }
}
