using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetCityControllerTest : BaseCityControllerTest
    {
        private const string BadName = "BadName";

        [Fact]
        public void GetCity_ItemExistAndPOIisRequested_ReturnCityWithPOIDTO()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var cityDto = new CityWithPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                POIList = city.PointsOfInterest,
                Population = city.Population
            };
            FakeCityRepository.GetCity(city.Name, true).Returns(city);


            var result = CityPoiController.GetCity(city.Name, true);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDto);
        }

        [Fact]
        public void GetCity_ItemExistAndPOIisNotRequested_ReturnCityWithPOIDTO()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var cityDto = new CityWithNoPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            };
            FakeCityRepository.GetCity(city.Name, false).Returns(city);


            var result = CityPoiController.GetCity(city.Name, false);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDto);
        }

        [Fact]
        public void GetCity_ItemDoesntExistAndPOIisRequested_ReturnNotFoundResult()
        {
            var result = CityPoiController.GetCity(BadName, true);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCity_ItemExistAndPOIisNotRequested_ReturnCityWithNoPOIDTO()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var cityDto = new CityWithNoPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            };
            FakeCityRepository.GetCity(city.Name, false).Returns(city);


            var result = CityPoiController.GetCity(city.Name, false);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDto);
        }

        [Fact]
        public void GetCity_ItemDoesntExistAndPOIisNotRequested_ReturnNotFoundResult()
        {
            var result = CityPoiController.GetCity(BadName, false);


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
