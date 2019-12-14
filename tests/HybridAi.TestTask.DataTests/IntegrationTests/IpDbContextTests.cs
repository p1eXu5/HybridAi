using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.UpdaterService;
using HybridAi.TestTask.DataTests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HybridAi.TestTask.DataTests.IntegrationTests
{
    [ TestFixture ]
    public class IpDbContextTests
    {
        [ OneTimeSetUp ]
        public void Setup()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
            DbContextOptionsFactory.Instance.ConnectionString =
                "Host=localhost;Database=TestIpDb;Username=testuser;Password=123;IntegratedSecurity=true";
        }

        [ TearDown ]
        public void DeleteDatabase()
        {
            using var context = new IpDbContext();

            context.Database.EnsureDeleted();
        }

        [ Test ]
        public void class_ByDefault_CanCreateDatabase()
        {
            using var context = new IpDbContext();

            context.Database.Migrate();
        }

        [ Test ]
        public void class_CityLocationWithoutCities_CanAddCityLocation()
        {
            using (var ctxForSeed = new IpDbContext()) 
            {
                ctxForSeed.Database.Migrate();

                var seededCityLocation = new CityLocation( 1 ) {
                    ContinentCode = "AB",
                    CountryIsoCode = "AB",
                    TimeZone = "Test Time Zone"
                };

                ctxForSeed.Add( seededCityLocation );
                ctxForSeed.SaveChanges();
            }

            using var ctxAssert = new IpDbContext();
            var clk = ctxAssert.GetCityLocations( 1, 1 ).ToArray();
            Assert.NotNull( clk );
        }

        #region factories

        #endregion
    }
}
