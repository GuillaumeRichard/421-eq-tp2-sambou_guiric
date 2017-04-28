using System.Collections.Generic;
using System.Linq;
using Bogus;
using CityPoiAPI.Controllers;
using CityPoiAPI.Entities;

namespace Tests.ItemBuilder
{
    public class CityPoiItemBuilder
    {

        private readonly Faker<City> _cityFaker;
        private readonly Faker<PointOfInterest> _poiFaker;
        private readonly Faker<PostPOIDTO> _postPoidtoFaker;

        public CityPoiItemBuilder()
        {
            _cityFaker = new Faker<City>(locale: "fr")
                          .RuleFor(o => o.Id, f => f.UniqueIndex)
                          .RuleFor(o => o.Name, f => f.Lorem.Word())
                          .RuleFor(o => o.Country, f => f.Lorem.Word())
                          .RuleFor(o => o.Population, f => f.Random.Int());

            _poiFaker = new Faker<PointOfInterest>(locale: "fr")
                .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.Id, f => f.UniqueIndex)
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.Longitude, f => f.Address.Longitude().ToString())
                .RuleFor(o => o.Latitude, f => f.Address.Latitude().ToString())
                .RuleFor(o => o.CityName, f => f.Lorem.Word());

            _postPoidtoFaker = new Faker<PostPOIDTO>(locale: "fr")
                .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.Longitude, f => f.Address.Longitude().ToString())
                .RuleFor(o => o.CityName, f => f.Lorem.Word())
                .RuleFor(o => o.Latitude, f => f.Address.Latitude().ToString());
        }

        public City GenerateCity()
        {
            var city = _cityFaker.Generate();
            var poiList = GeneratePoiList(3, city);
            city.PointsOfInterest = poiList;
            return city;

        }

        public List<City> GenerateCityList(int listLength)
        {
            var cityList = _cityFaker.Generate(listLength).ToList();
            foreach (var element in cityList)
            {
                element.PointsOfInterest = GeneratePoiList(3, element);
            }
            return cityList;
        }

        private List<PointOfInterest> GeneratePoiList(int listLength, City city)
        {
            var poiList = _poiFaker.Generate(listLength).ToList();
            foreach(var element in poiList)
            {
                element.CityName = city.Name;
            }
            return poiList;
        }

        public PointOfInterest GeneratePointOfInterest()
        {
            return _poiFaker.Generate();
        }

        public PostPOIDTO GeneratePostPoidto()
        {
            return _postPoidtoFaker.Generate();
        }

    }
}

