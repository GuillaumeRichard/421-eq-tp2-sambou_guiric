using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CityPoiAPI.Entities;
using FluentAssertions;
using Tests.ControllerTests;
using Xunit;

namespace Tests.ModelStateTests
{
    public class CityModelStateTest : BaseCityControllerTest
    {

        [Fact]
        public void ModelState_GoodItem_ReturnNoError()
        {
            var city = CityPoiItemBuilder.GenerateCity();

            var modelStateValidity = ValidateCity(city);

            modelStateValidity.Should().BeTrue();
        }

        [Fact]
        public void ModelState_CityWithoutName_ReturnAnError()
        {
            var city = CityPoiItemBuilder.GenerateCity();
            city.Name = null;

            var modelStateValidity = ValidateCity(city);

            modelStateValidity.Should().BeFalse();
        }

        [Fact]
        public void ModelState_CityWithoutCountry_ReturnAnError()
        {
            var city = CityPoiItemBuilder.GenerateCity();
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
