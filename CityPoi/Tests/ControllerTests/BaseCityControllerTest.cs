using NSubstitute;
using CityPoiAPI.Controllers;
using CityPoiAPI.Services;
using CityPoiAPI.DTO;

namespace Tests
{
    public class BaseCityControllerTest
    {

        protected ICityRepository _fakeCityRepository;
        protected CityPoiController _cityPoiController;
        protected CityPoiItemBuilder _cityPoiItemBuilder;
        protected DTOMapper _DTOMapper;

        public BaseCityControllerTest()
        {
            _fakeCityRepository = Substitute.For<ICityRepository>();
            _cityPoiController = new CityPoiController(_fakeCityRepository);
            _cityPoiItemBuilder = new CityPoiItemBuilder();
            _DTOMapper = new DTOMapper();
        }
    }
}
