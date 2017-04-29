using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace Tests.ControllerTests
{
    public class PutPoiControllerTest: BaseCityControllerTest
    {
        private const int GoodId = 1; 
        private const int NotMatchingId = 2;
        private const int BadId = -999;
        private const bool IncludePointsOfInterest = true;

        [Fact]
        public void UpdatePointOfInterest_PoiIsNull_ReturnBadRequest()
        {
            var result = PoiController.UpdatePointOfInterest(GoodId, null);


            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Update_IdDoesNotMatchItemId_ReturnBadRequest()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            var poiDto = DtoMapper.PoiToPoiDto(poi);


            var result = PoiController.UpdatePointOfInterest(NotMatchingId, poiDto);


            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_PoiNotFound_ReturnNotFound()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Id = BadId;
            poi.CityId = city.Id;
            var poiDto = DtoMapper.PoiToPoiDto(poi);
            FakeCityRepository.GetCity(city.Id, IncludePointsOfInterest).Returns(city);


            var result = PoiController.UpdatePointOfInterest(BadId, poiDto);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_PoiFound_CallsUpdateOnRepository()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            city.PointsOfInterest.Add(poi);
            poi.CityId = city.Id;
            var poiDto = DtoMapper.PoiToPoiDto(poi);
            FakeCityRepository.GetCity(city.Id, IncludePointsOfInterest).Returns(city);

            var result = PoiController.UpdatePointOfInterest(poi.Id, poiDto);

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
            var result = PoiController.UpdatePointOfInterest(poi.Id, poiDto);

            //Assert 
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
