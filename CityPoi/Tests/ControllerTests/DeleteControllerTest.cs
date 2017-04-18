﻿using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class DeleteControllerTest : BaseCityControllerTest
    {
        private int badId = 0;

        [Fact]
        public void Delete_CityAndPoiIsInRepository_DeletePoiInRepository()
        {
            //Arrange
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.GetCity(city.Id, true).Returns(city);
            _fakeCityRepository.CityExists(city.Id).Returns(true);


            //Action
            _cityPoiController.DeletePointOfIntetest(city.Id, city.PointsOfInterest.First().Id);


            // Assert
            _fakeCityRepository.Received().DeletePointOfInterest(city.PointsOfInterest.First());
        }

        [Fact]
        public void Delete_CityIsNotInRepository_ReturnsNotFound()
        {
            //Arrange
            var city = _cityPoiItemBuilder.GenerateCity();

            //Action
            var result = _cityPoiController.DeletePointOfIntetest(city.Id, city.PointsOfInterest.First().Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_PoiIsNotInRepository_ReturnsNotFound()
        {
            //Arrange
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);
            _fakeCityRepository.GetCity(city.Id, true).Returns(city);

            //Action
            var result = _cityPoiController.DeletePointOfIntetest(city.Id, badId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_ItemIsInRepository_ReturnNoContentResult()
        {
            //Arrange
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);
            _fakeCityRepository.GetCity(city.Id, true).Returns(city);


            //Action
            var result = _cityPoiController.DeletePointOfIntetest(city.Id, city.PointsOfInterest.First().Id);


            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
