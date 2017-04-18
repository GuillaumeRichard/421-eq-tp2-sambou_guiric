using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetCityControllerTest : BaseCityControllerTest
    {
        private const int Bad_Id = 0;

        [Fact]
        public void GetCity_ItemExistAndPOIisRequested_ReturnCityWithPOIDTO()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var cityDTO = new CityWithPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                POIList = city.PointsOfInterest,
                Population = city.Population
            };
            _fakeCityRepository.GetCity(city.Id, true).Returns(city);


            var result = _cityPoiController.GetCity(city.Id, true);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDTO);
        }

        [Fact]
        public void GetCity_ItemExistAndPOIisNotRequested_ReturnCityWithPOIDTO()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var cityDTO = new CityWithNoPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            };
            _fakeCityRepository.GetCity(city.Id, false).Returns(city);


            var result = _cityPoiController.GetCity(city.Id, false);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDTO);
        }

        [Fact]
        public void GetCity_ItemDoesntExistAndPOIisRequested_ReturnNotFoundResult()
        {     
            var result = _cityPoiController.GetCity(Bad_Id, true);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCity_ItemExistAndPOIisNotRequested_ReturnCityWithNoPOIDTO()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var cityDTO = new CityWithNoPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            };
            _fakeCityRepository.GetCity(city.Id, false).Returns(city);


            var result = _cityPoiController.GetCity(city.Id, false);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(cityDTO);
        }

        [Fact]
        public void GetCity_ItemDoesntExistAndPOIisNotRequested_ReturnNotFoundResult()
        {
            var result = _cityPoiController.GetCity(Bad_Id, false);


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
