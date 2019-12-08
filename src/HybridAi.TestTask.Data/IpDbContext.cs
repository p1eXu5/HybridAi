using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.Data.Configurations;
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

        public DbSet< CityBlock > CityBlockIpv4Collection { get; set; }
        public DbSet< CityBlockIpv6 > CityBlockIpv6Collection { get; set; }
        public DbSet< CityLocation > CityLocations { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if ( !optionsBuilder.IsConfigured ) {
                optionsBuilder.UseNpgsql( "Host=localhost;Database=TestIpDb;Username=testuser;Password=123;IntegratedSecurity=true" );
            }
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new CityBlockIpv4Configuration() )
                        .ApplyConfiguration( new CityBlockIpv6Configuration() )
                        .ApplyConfiguration( new CityLocationConfiguration() )
                        .ApplyConfiguration( new EnCityConfiguration() )
                        .ApplyConfiguration( new LocaleCodeConfiguration() );
        }
    }
}
