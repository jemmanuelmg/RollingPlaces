using Microsoft.EntityFrameworkCore;
using RollingPlaces.Web.Data.Entities;

namespace RollingPlaces.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<PlaceEntity> Places { get; set; }

        public DbSet<QualificationEntity> Qualifications { get; set; }

        public DbSet<PhotoEntity> Photos { get; set; }

        
    }
}
