/*
 * This product includes GeoLite2 data created by MaxMind, available from
 * <a href="https://www.maxmind.com">https://www.maxmind.com</a>.
 */


using HybridAi.TestTask.Data.Configurations;
using HybridAi.TestTask.Data.Configurations.Cities;
using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.Data
{
    public class IpDbContext : DbContext
    {
        public IpDbContext()
        { }

        public IpDbContext( DbContextOptions< IpDbContext > options )
            :base( options )
        { }

        public DbSet< CityBlockIpv6 > CityBlockIpv6Collection { get; set; }
        public DbSet< CityBlockIpv4 > CityBlockIpv4Collection { get; set; }
        public DbSet< CityLocation > CityLocations { get; set; }
        public DbSet< EnCity > EnCities { get; set; }
        public DbSet< RuCity > RuCities { get; set; }
        public DbSet< DeCity > DeCities { get; set; }
        public DbSet< FrCity > FrCities { get; set; }
        public DbSet< EsCity > EsCities { get; set; }
        public DbSet< JaCity > JaCities { get; set; }
        public DbSet< PtBrCity > PtBrCities { get; set; }
        public DbSet< ZhCnCity > ZhCnCities { get; set; }
        public DbSet< LocaleCode > LocaleCodes { get; set; }


        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if ( !optionsBuilder.IsConfigured ) {
                optionsBuilder.UseNpgsql( DbContextOptionsFactory.DEFAULT_CONNECTION_STRING );
            }
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new CityBlockIpv4Configuration() )
                        .ApplyConfiguration( new CityBlockIpv6Configuration() )
                        .ApplyConfiguration( new CityLocationConfiguration() )
                        .ApplyConfiguration( new EnCityConfiguration() )
                        .ApplyConfiguration( new RuCityConfiguration() )
                        .ApplyConfiguration( new DeCityConfiguration() )
                        .ApplyConfiguration( new EsCityConfiguration() )
                        .ApplyConfiguration( new FrCityConfiguration() )
                        .ApplyConfiguration( new JaCityConfiguration() )
                        .ApplyConfiguration( new PtBrCityConfiguration() )
                        .ApplyConfiguration( new ZhCnCityConfiguration() )
                        .ApplyConfiguration( new LocaleCodeConfiguration() );
        }
    }
}
