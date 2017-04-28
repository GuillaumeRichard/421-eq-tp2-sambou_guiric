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

        private const string BadName = "badname";

        [Fact]
        public void GetPoi_PoiExist_ReturnPointOfInterestDTO()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            var poiDto = new PointOfInterestDTO
            {
                CityName = poi.CityName,
                Name = poi.Name,
                Address = poi.Address,
                Description = poi.Description,
                Id = poi.Id,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude

            };
            FakeCityRepository.GetPointOfInterestForCity(city.Name, poi.Name).Returns(poi);
            FakeCityRepository.CityExists(city.Name).Returns(true);


            var result = PoiController.GetPointOfInterest(city.Name, poi.Name);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(poiDto);
        }

        [Fact]
        public void GetPoi_PoiDoesNotExist_ReturnNotFoundResult()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Name).Returns(true);


            var result = PoiController.GetPointOfInterest(city.Name, BadName);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetPoi_CityDoesNotExist_ReturnNotFoundResult()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            FakeCityRepository.CityExists(city.Name).Returns(false);


            var result = PoiController.GetPointOfInterest(city.Name, poi.Name);


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}

