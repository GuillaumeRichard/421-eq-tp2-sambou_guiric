using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;
using CityPoiAPI.Entities;
using CityPoiAPI.DTO;
using NSubstitute;

namespace Tests.ControllerTests
{
    public class PutPoiControllerTest: BaseCityControllerTest
    {
        private const int _Good_Id = 1; 
        private const int _Not_Matching_Id = 2;
        private const int _Bad_Id = -999;
        private const bool _IncludePointsOfInterest = true;

        public PutPoiControllerTest() : base()
        {
        }

        [Fact]
        public void UpdatePointOfInterest_PoiIsNull_ReturnBadRequest()
        {
            var result = _poiController.UpdatePointOfInterest(_Good_Id, null);


            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Update_IdDoesNotMatchItemId_ReturnBadRequest()
        {
            var poi = _cityPoiItemBuilder.GeneratePointOfInterest();
            var poiDTO = _DTOMapper.PoiToPoiDto(poi);


            var result = _poiController.UpdatePointOfInterest(_Not_Matching_Id, poiDTO);


            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_PoiNotFound_ReturnNotFound()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var poi = _cityPoiItemBuilder.GeneratePointOfInterest();
            poi.Id = _Bad_Id;
            poi.CityId = city.Id;
            var poiDTO = _DTOMapper.PoiToPoiDto(poi);
            _fakeCityRepository.GetCity(city.Id, _IncludePointsOfInterest).Returns(city);


            var result = _poiController.UpdatePointOfInterest(_Bad_Id, poiDTO);


            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_PoiFound_CallsUpdateOnRepository()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            var poi = _cityPoiItemBuilder.GeneratePointOfInterest();
            city.PointsOfInterest.Add(poi);
            poi.CityId = city.Id;
            var poiDTO = _DTOMapper.PoiToPoiDto(poi);
            _fakeCityRepository.GetCity(city.Id, _IncludePointsOfInterest).Returns(city);

            var result = _poiController.UpdatePointOfInterest(poi.Id, poiDTO);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdatePointOfInterest_BadRequest_ReturnBadRequestObjectWithError()
        {
            //Arrange
            var poi = _cityPoiItemBuilder.GeneratePointOfInterest();
            var poiDTO = _DTOMapper.PoiToPoiDto(poi);
            _poiController.ModelState.AddModelError("Error", "Model state error");

            //Action
            var result = _poiController.UpdatePointOfInterest(poi.Id, poiDTO);

            //Assert 
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
