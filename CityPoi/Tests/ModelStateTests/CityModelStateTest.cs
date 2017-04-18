using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using CityPoiAPI.Entities;
using Xunit;

namespace Tests
{
    public class CityModelStateTest : BaseCityControllerTest
    {

        [Fact]
        public void ModelState_GoodItem_ReturnNoError()
        {
            var city = _cityPoiItemBuilder.GenerateCity();

            var modelStateValidity = ValidateCity(city);

            modelStateValidity.Should().BeTrue();
        }

        [Fact]
        public void ModelState_CityWithoutName_ReturnAnError()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            city.Name = null;

            var modelStateValidity = ValidateCity(city);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_CityWithoutCountry_ReturnAnError()
        {
            var city = _cityPoiItemBuilder.GenerateCity();
            city.Country = null;

            var modelStateValidity = ValidateCity(city);

            modelStateValidity.Should().BeFalse();
        }

        private bool ValidateCity(City city)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(city, null, null);
            return Validator.TryValidateObject(city, validationContext, validationResults, true);
        }
    }
}
