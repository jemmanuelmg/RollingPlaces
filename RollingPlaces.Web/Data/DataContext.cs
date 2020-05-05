using Microsoft.EntityFrameworkCore;
using RollingPlaces.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RollingPlaces.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
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
