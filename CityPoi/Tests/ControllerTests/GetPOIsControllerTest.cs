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
            FakeCityRepository.GetPointsOfInterestForCity(city.Id).Returns(city.PointsOfInterest);
            FakeCityRepository.CityExists(city.Id).Returns(true);

            var poiDto = new PointsOfInterestDto
            {
                PoiList = city.PointsOfInterest
            };

            //Action
            var result = PoiController.GetPointsOfInterestForCity(city.Id);


            // Assert
            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(poiDto);
        }

        [Fact]
        public void GetPoiList_CityNotFound_ReturnNotFound()
        {
            //Arrange
            const int badId = -99999999;

            //Action
            var result = PoiController.GetPointsOfInterestForCity(badId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetPoiList_NoPoiFound_ReturnObjectNotFound()
        {
            //Arrange
            var city = CityPoiItemBuilder.GenerateCity();
            city.PointsOfInterest = null;
            FakeCityRepository.GetPointsOfInterestForCity(city.Id).Returns(city.PointsOfInterest);

            //Action
            var result = PoiController.GetPointsOfInterestForCity(city.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
