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
        public void GetCities_CititesExist_ReturnCityList() //YM: ReturnCityList --> ReturnCityDtos
        {
            //Arrange
            var listLength = 10; //YM: valeur qui n'a pas à être modifiée --> mettre en constante 
            var cities = _cityPoiItemBuilder.GenerateCityList(listLength);
            var DTOList = cities.Select(city => new CityWithNoPOIDTO  //YM: Dans tous les tests, utiliser un mapper pour améliorer la lisibilité
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
