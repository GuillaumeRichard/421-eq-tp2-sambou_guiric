using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Tests.ControllerTests
{
    public class PostPoiControllerTest: BaseCityControllerTest
    {
        private const int BadCityId = -1;

        [Fact]
        public void AddPOI_POIIsNull_ReturnBadRequest()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);


            var result = PoiController.AddPointOfInterestToCity(city.Id, null);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void AddPOI_POIExist_CallsAddOnRepository()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            var poiDto = DtoMapper.PoiToPostPoiDto(poi);

            var result = PoiController.AddPointOfInterestToCity(city.Id, poiDto);

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public void AddPOI_BadItem_ReturnBadRequestWithError() 
        {
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);
            var poi = CityPoiItemBuilder.GeneratePostPoidto();
            PoiController.ModelState.AddModelError("Error", "Model state error");  

            var result = PoiController.AddPointOfInterestToCity(city.Id, poi);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void AddPOI_BadAddress_ReturnBadRequestWithError()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            FakeCityRepository.CityExists(city.Id).Returns(true);
            var poiDto = CityPoiItemBuilder.GeneratePostPoidto();

            var result = PoiController.AddPointOfInterestToCity(BadCityId, poiDto);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
