using System.Linq;
using FluentAssertions;
using NSubstitute;
using Xunit;
using CityPoiAPI.DTO;

namespace Tests.ControllerTests
{
    public class GetAllControllerTest : BaseCityControllerTest
    {

        [Fact]
        public void GetCities_CititesExist_ReturnCityDtos()
        {
            //Arrange
            const int listLength = 10;
            var cities = _cityPoiItemBuilder.GenerateCityList(listLength);
            var dtoList = cities.Select(city => new CityWithNoPOIDTO  //YM: Dans tous les tests, utiliser un mapper pour améliorer la lisibilité
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
            result.ShouldBeEquivalentTo(dtoList);
        }
    }
}
