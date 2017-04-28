using CityPoiAPI.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.ControllerTests
{
    public class GetPoIsControllerTest : BaseCityControllerTest
    {

        [Fact]
        public void GetPOIList_CityExist_ReturnPOIList()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.GetPointsOfInterestForCity(city.Name).Returns(city.PointsOfInterest);
            FakeCityRepository.CityExists(city.Name).Returns(true);

            var poiDto = new PointsOfInterestDTO
            {
                POIList = city.PointsOfInterest
            };

            //Action
            var result = PoiController.GetPointsOfInterestForCity(city.Name);


            // Assert
            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(poiDto);
        }

        [Fact]
        public void GetPoiList_CityNotFound_ReturnNotFound()
        {
            //Arrange
            const string badName = "badName";

            //Action
            var result = PoiController.GetPointsOfInterestForCity(badName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetPoiList_NoPoiFound_ReturnObjectNotFound()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();
            city.PointsOfInterest = null;
            FakeCityRepository.GetPointsOfInterestForCity(city.Name).Returns(city.PointsOfInterest);

            //Action
            var result = PoiController.GetPointsOfInterestForCity(city.Name);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
