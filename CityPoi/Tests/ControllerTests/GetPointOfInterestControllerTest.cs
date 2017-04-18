using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetPointOfInterestControllerTest : BaseCityControllerTest
    {

        private const int Bad_Id = 0;

        [Fact]
        public void GetPoi_PoiExist_ReturnPointOfInterestDTO()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            var POIDTO = new PointOfInterestDTO
            {
                CityId = poi.CityId,
                Name = poi.Name,
                Address = poi.Address,
                Description = poi.Description,
                Id = poi.Id,
                Latitude = poi.Latitude,
                Longitude = poi.Longitude

            };
            _fakeCityRepository.GetPointOfInterestForCity(city.Id, poi.Id).Returns(poi);
            _fakeCityRepository.CityExists(city.Id).Returns(true);


            var result = _cityPoiController.GetPointOfInterest(city.Id, poi.Id);


            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(POIDTO);
        }

        [Fact]
        public void GetPoi_PoiDoesNotExist_ReturnNotFoundResult()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);


            var result = _cityPoiController.GetPointOfInterest(city.Id, Bad_Id);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetPoi_CityDoesNotExist_ReturnNotFoundResult()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            _fakeCityRepository.CityExists(city.Id).Returns(false);


            var result = _cityPoiController.GetPointOfInterest(city.Id, poi.Id);


            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
