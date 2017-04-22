using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CityPoiAPI.Entities;
using Xunit;
using FluentAssertions;
using Tests.ControllerTests;

namespace Tests
{
    public class PoiModelStateTest : BaseCityControllerTest
    {
        [Fact]
        public void ModelState_GoodPOI_ReturnNoError()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeTrue();
        }

        [Fact]
        public void ModelState_POIWithoutName_ReturnAnError()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Name = null;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_POIWithoutAddress_ReturnAnError()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Address = null;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_POIWithoutDescription_ReturnAnError()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Description = null;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_POIWithoutLongitude_ReturnAnError()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Longitude = null;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_POIWithoutLatitude_ReturnAnError()
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Latitude = null;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData("bonjour")]
        [InlineData("999")]
        [InlineData("888.88")]
        [InlineData("180.0001")]
        [InlineData("190.0000")]
        public void ModelState_BadLongitude_ReturnAnError(string longitude)
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Longitude = longitude;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData("bonjour")]
        [InlineData("999")]
        [InlineData("888.88")]
        [InlineData("90.0001")]
        [InlineData("100.0000")]
        public void ModelState_BadLatitude_ReturnAnError(string latitude)
        {
            var poi = CityPoiItemBuilder.GeneratePointOfInterest();
            poi.Latitude = latitude;

            var modelStateValidity = ValidatePoi(poi);

            modelStateValidity.Should().BeFalse();
        }

        private bool ValidatePoi(PointOfInterest poi)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(poi, null, null);
            return Validator.TryValidateObject(poi, validationContext, validationResults, true);
        }

    }
}
