using CityPoiAPI.Controllers;
using CityPoiAPI.DTO;
using CityPoiAPI.Services;
using NSubstitute;
using Tests.ItemBuilder;

namespace Tests.ControllerTests
{
    public class BaseCityControllerTest
    {

        protected ICityRepository FakeCityRepository;
        protected CityPoiController CityPoiController;
        protected PoiController PoiController;
        protected CityPoiItemBuilder CityPoiItemBuilder;
        protected DtoMapper DtoMapper;

        public BaseCityControllerTest()
        {
            FakeCityRepository = Substitute.For<ICityRepository>();
            CityPoiController = new CityPoiController(FakeCityRepository);
            PoiController = new PoiController(FakeCityRepository);
            CityPoiItemBuilder = new CityPoiItemBuilder();
            DtoMapper = new DtoMapper();
        }
    }
}
