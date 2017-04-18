using CityPoiAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityPoiAPI.DataAccessLayer
{
    public class ApiDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
    }
}
