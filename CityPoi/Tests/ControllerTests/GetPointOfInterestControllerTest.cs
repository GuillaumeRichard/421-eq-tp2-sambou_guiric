using System.Linq;
using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetPointOfInterestControllerTest : BaseCityControllerTest
    {

        private const int BadId = 0;

        [Fact]
        public void GetPoi_PoiExist_ReturnPointOfInterestDTO()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            var poiDto = new PointOfInterestDTO
            {
                CityId = poi.CityId,
                Name = poi.Name,
                Address = poi.Address,
                Description = poi.Description,
                Id = poi.Id,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude

            };
            FakeCityRepository.GetPointOfInterestForCity(city.Id, poi.Id).Returns(poi);
            FakeCityRepository.CityExists(city.Id).Returns(true);


            var result = PoiController.GetPointOfInterest(city.Id, poi.Id);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(poiDto);
        }

        [Fact]
        public void GetPoi_PoiDoesNotExist_ReturnNotFoundResult()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);


            var result = PoiController.GetPointOfInterest(city.Id, BadId);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetPoi_CityDoesNotExist_ReturnNotFoundResult()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            FakeCityRepository.CityExists(city.Id).Returns(false);


            var result = PoiController.GetPointOfInterest(city.Id, poi.Id);


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}

