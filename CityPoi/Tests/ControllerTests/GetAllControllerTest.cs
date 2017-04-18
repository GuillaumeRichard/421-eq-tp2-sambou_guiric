using System.Collections.Generic;
using System.Linq;
using CityPoiAPI.DTO;
using CityPoiAPI.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.GetAllTest
{
    public class GetAllControllerTest : BaseCityControllerTest
    {

        [Fact]
        public void GetCities_CititesExist_ReturnCityList()
        {
            //Arrange
            var listLength = 10;
            var cities = _cityPoiItemBuilder.GenerateCityList(listLength);
            var DTOList = cities.Select(city => new CityWithNoPOIDTO
            {
                CityId = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population
            }).ToList();
            _fakeCityRepository.GetCities().Returns(cities);


            //Action
            var result = _cityPoiController.GetAll();


            // Assert
            result.ShouldBeEquivalentTo(DTOList);
        }
    }
}
