using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Models
{
    public class CityContext : DbContext
    {
        public CityContext()
        {
        }
        public CityContext(DbContextOptions<CityContext> options)
            : base(options)
        {
        }
        public virtual DbSet<City> City { get; set; } = null!;
    }
}
