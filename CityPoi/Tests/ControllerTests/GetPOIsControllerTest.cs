using System;
using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetPOIsControllerTest : BaseCityControllerTest
    {

        [Fact]
        public void GetPOIList_CityExist_ReturnPOIList()
        {
            //Arrange
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.GetPointsOfInterestForCity(city.Id).Returns(city.PointsOfInterest);
            _fakeCityRepository.CityExists(city.Id).Returns(true);

            var POIDTO = new PointsOfInterestDTO
            {
                POIList = city.PointsOfInterest
            };

            //Action
            var result = _cityPoiController.GetPointsOfInterestForCity(city.Id);


            // Assert
            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(POIDTO);
        }

        [Fact]
        public void GetPoiId_NoPoiFound_ReturnObjectNotFound()
        {
            //Arrange
            var badId = -99999999;

            //Action
            var result = _cityPoiController.GetPointsOfInterestForCity(badId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
