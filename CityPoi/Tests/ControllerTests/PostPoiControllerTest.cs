using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Tests.ControllerTests
{
    public class PostPoiControllerTest: BaseCityControllerTest
    {
        private const int Any_City_Id = 1;
        private const int Bad_City_Id = -1;

        [Fact]
        public void AddPOI_POIIsNull_ReturnBadRequest()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);


            var result = _cityPoiController.AddPointOfInterestToCity(city.Id, null);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void AddPOI_POIExist_CallsAddOnRepository()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);
            var poi = _cityPoiItemBuilder.GeneratePointOfInterest();
            var poiDTO = _DTOMapper.PoiToPostPoiDto(poi);

            var result = _cityPoiController.AddPointOfInterestToCity(city.Id, poiDTO);

            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public void AddPOI_BadItem_ReturnBadRequestWithError()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);
            var poi = _cityPoiItemBuilder.GeneratePostPOIDTO();
            _cityPoiController.ModelState.AddModelError("Error", "Model state error");

            var result = _cityPoiController.AddPointOfInterestToCity(city.Id, poi);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void AddPOI_BadAddress_ReturnBadRequestWithError()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            _fakeCityRepository.CityExists(city.Id).Returns(true);
            var poiDTO = _cityPoiItemBuilder.GeneratePostPOIDTO();

            var result = _cityPoiController.AddPointOfInterestToCity(Bad_City_Id, poiDTO);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
