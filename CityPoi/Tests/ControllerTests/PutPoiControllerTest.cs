using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace Tests.ControllerTests
{
    public class PutPoiControllerTest: BaseCityControllerTest
    {
        private const string GoodName = "GoodName"; 
        private const string NotMatchingName = "NotMatchingName";
        private const string BadName = "BadName";
        private const bool IncludePointsOfInterest = true;

        [Fact]
        public void UpdatePointOfInterest_PoiIsNull_ReturnBadRequest()
        {
            var result = PoiController.UpdatePointOfInterest(GoodName, null);


            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Update_NameDoesNotMatchItemName_ReturnBadRequest()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            var poiDto = DtoMapper.PoiToPoiDto(poi);


            var result = PoiController.UpdatePointOfInterest(NotMatchingName, poiDto);


            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_PoiNotFound_ReturnNotFound()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Name = BadName;
            poi.CityName = city.Name;
            var poiDto = DtoMapper.PoiToPoiDto(poi);
            FakeCityRepository.GetCity(city.Name, IncludePointsOfInterest).Returns(city);


            var result = PoiController.UpdatePointOfInterest(BadName, poiDto);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_PoiFound_CallsUpdateOnRepository()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            city.PointsOfInterest.Add(poi);
            poi.CityName = city.Name;
            var poiDto = DtoMapper.PoiToPoiDto(poi);
            FakeCityRepository.GetCity(city.Name, IncludePointsOfInterest).Returns(city);

            var result = PoiController.UpdatePointOfInterest(poi.Name, poiDto);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_BadRequest_ReturnBadRequestObjectWithError()
        {
            //Arrange
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            var poiDto = DtoMapper.PoiToPoiDto(poi);
            PoiController.ModelState.AddModelError("Error", "Model state error");

            //Action
            var result = PoiController.UpdatePointOfInterest(poi.Name, poiDto);

            //Assert 
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
