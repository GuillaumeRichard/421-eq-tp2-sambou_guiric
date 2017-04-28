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
            var poi = city.PointsOfInterest.First();
            FakeCityRepository.GetCity(city.Name, true).Returns(city);
            FakeCityRepository.GetPointOfInterestForCity(city.Name, poi.Name).Returns(poi);
            FakeCityRepository.CityExists(city.Name).Returns(true);


            //Action
            PoiController.DeletePointOfIntetest(city.Name, poi.Name);


            // Assert
            FakeCityRepository.Received().DeletePointOfInterest(city.PointsOfInterest.First());
        }

        [Fact]
        public void Delete_CityIsNotInRepository_ReturnsNotFound()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();

            //Action
            var result = PoiController.DeletePointOfIntetest(city.Name, city.PointsOfInterest.First().Name);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_PoiIsNotInRepository_ReturnsNotFound()
        {   
            const string badName = "badName";
            //Arrange
             var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Name).Returns(true);
            FakeCityRepository.GetCity(city.Name, true).Returns(city);

            //Action
            var result = PoiController.DeletePointOfIntetest(city.Name, badName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_ItemIsInRepository_ReturnNoContentResult()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = city.PointsOfInterest.First();
            FakeCityRepository.CityExists(city.Name).Returns(true);
            FakeCityRepository.GetPointOfInterestForCity(city.Name, poi.Name).Returns(poi);
            FakeCityRepository.GetCity(city.Name, true).Returns(city);


            //Action
            var result = PoiController.DeletePointOfIntetest(city.Name, city.PointsOfInterest.First().Name);


            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
