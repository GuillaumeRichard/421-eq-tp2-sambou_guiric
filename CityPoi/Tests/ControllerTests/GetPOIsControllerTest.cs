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

            var poiDto = new PointsOfInterestDTO
            {
                POIList = city.PointsOfInterest
            };

            //Action
            var result = PoiController.GetPointsOfInterestForCity(city.Id);


            // Assert
            result.Should().BeOfType<ObjectResult>().Which.Value.ShouldBeEquivalentTo(poiDto);
        }

        [Fact]
        public void GetPoiId_NoPoiFound_ReturnObjectNotFound()
        {
            //Arrange
            var badId = -99999999;

            //Action
            var result = PoiController.GetPointsOfInterestForCity(badId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
