using System;
using Bogus;
using System.Collections.Generic;
using System.Linq;
using CityPoiAPI.Entities;
using CityPoiAPI.Controllers;

namespace Tests
{
    public class CityPoiItemBuilder
    {

        private Faker<City> _cityFaker;
        private Faker<PointOfInterest> _POIFaker;
        private Faker<PostPOIDTO> _PostPOIDTOFaker;

        public CityPoiItemBuilder()
        {
            _cityFaker = new Faker<City>(locale: "fr")
                          .RuleFor(o => o.Id, f => f.UniqueIndex)
                          .RuleFor(o => o.Name, f => f.Lorem.Word())
                          .RuleFor(o => o.Country, f => f.Lorem.Word())
                          .RuleFor(o => o.Population, f => f.Random.Int());

            _POIFaker = new Faker<PointOfInterest>(locale: "fr")
                .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.Id, f => f.UniqueIndex)
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.Longitude, f => f.Address.Longitude().ToString())
                .RuleFor(o => o.Latitude, f => f.Address.Latitude().ToString());

            _PostPOIDTOFaker = new Faker<PostPOIDTO>(locale: "fr")
                .RuleFor(o => o.Address, f => f.Address.StreetAddress())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.Longitude, f => f.Address.Longitude().ToString())
                .RuleFor(o => o.CityId, f => f.UniqueIndex)
                .RuleFor(o => o.Latitude, f => f.Address.Latitude().ToString());
        }

        public City GenerateCity()
        {
            City city = _cityFaker.Generate();
            List<PointOfInterest> POIList = GeneratePoiList(3, city);
            city.PointsOfInterest = POIList;
            return city;

        }

        public List<City> GenerateCityList(int listLength)
        {
            List<City> cityList = _cityFaker.Generate(listLength).ToList();
            foreach (City element in cityList)
            {
                element.PointsOfInterest = GeneratePoiList(3, element);
            }
            return cityList;
        }

        private List<PointOfInterest> GeneratePoiList(int listLength, City city)
        {
            List<PointOfInterest> POIList = _POIFaker.Generate(listLength).ToList();
            foreach(PointOfInterest element in POIList)
            {
                element.CityId = city.Id;
            }
            return POIList;
        }

        public PointOfInterest GeneratePointOfInterest()
        {
            return _POIFaker.Generate();
        }

        public PostPOIDTO GeneratePostPOIDTO()
        {
            return _PostPOIDTOFaker.Generate();
        }

    }
}
