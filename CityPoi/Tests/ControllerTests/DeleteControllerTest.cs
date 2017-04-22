using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class DeleteControllerTest : BaseCityControllerTest
    {
        [Fact]
        public void Delete_CityAndPoiIsInRepository_CallDeletePoiInRepository()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.GetCity(city.Id, true).Returns(city);
            FakeCityRepository.CityExists(city.Id).Returns(true);


            //Action
            PoiController.DeletePointOfIntetest(city.Id, city.PointsOfInterest.First().Id);


            // Assert
            FakeCityRepository.Received().DeletePointOfInterest(city.PointsOfInterest.First());
        }

        [Fact]
        public void Delete_CityIsNotInRepository_ReturnsNotFound()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();

            //Action
            var result = PoiController.DeletePointOfIntetest(city.Id, city.PointsOfInterest.First().Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_PoiIsNotInRepository_ReturnsNotFound()
        {
            const int badId = 0;
            //Arrange
             var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);
            FakeCityRepository.GetCity(city.Id, true).Returns(city);

            //Action
            var result = PoiController.DeletePointOfIntetest(city.Id, badId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_ItemIsInRepository_ReturnNoContentResult()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);
            FakeCityRepository.GetCity(city.Id, true).Returns(city);


            //Action
            var result = PoiController.DeletePointOfIntetest(city.Id, city.PointsOfInterest.First().Id);


            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
