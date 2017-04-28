using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetCityControllerTest : BaseCityControllerTest
    {
        private const int BadId = 0;

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
            FakeCityRepository.GetCity(city.Id, true).Returns(city);


            var result = CityPoiController.GetCity(city.Id, true);


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
            FakeCityRepository.GetCity(city.Id, false).Returns(city);


            var result = CityPoiController.GetCity(city.Id, false);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDto);
        }

        [Fact]
        public void GetCity_ItemDoesntExistAndPOIisRequested_ReturnNotFoundResult()
        {     
            var result = CityPoiController.GetCity(BadId, true);


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
            FakeCityRepository.GetCity(city.Id, false).Returns(city);


            var result = CityPoiController.GetCity(city.Id, false);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDto);
        }

        [Fact]
        public void GetCity_ItemDoesntExistAndPOIisNotRequested_ReturnNotFoundResult()
        {
            var result = CityPoiController.GetCity(BadId, false);


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
