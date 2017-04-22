using CityPoiAPI.Controllers;
using CityPoiAPI.DTO;
using CityPoiAPI.Services;
using NSubstitute;

namespace Tests.ControllerTests
{
    public class BaseCityControllerTest
    {

        protected ICityRepository FakeCityRepository;
        protected CityPoiController CityPoiController;
        protected PoiController PoiController;
        protected CityPoiItemBuilder CityPoiItemBuilder;
        protected DTOMapper DtoMapper;

        public BaseCityControllerTest()
        {
            FakeCityRepository = Substitute.For<ICityRepository>();
            CityPoiController = new CityPoiController(FakeCityRepository);
            CityPoiItemBuilder = new CityPoiItemBuilder();
            DtoMapper = new DTOMapper();
        }
    }
}
