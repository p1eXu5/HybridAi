using HybridAi.TestTask.ConsoleDbUpdater;
using NUnit.Framework;
using System.Linq;
using System.Runtime.CompilerServices;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.DataTests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.DataTests.UnitTests
{

	[TestFixture]
	public class IpDbContextTests
	{
        [ OneTimeSetUp ]
        public void Setup()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
        }


		[Test]
		public void SaveChanges_AddCityLocationToCityBlock_CanUpdateCity()
        {
            // Arrange:
            var cityBlock = new CityBlockIpv4( "1.1.1.1" );

            using ( var ctx = new IpDbContext( _getOptions() ) ) {
                ctx.Add( cityBlock );
                ctx.SaveChanges();
            }

            // Action:
            var cityLocation = new CityLocation( 1 ) {
                EnCity = new EnCity( 1 ),
                EsCity = new EsCity( 1 ),
            };

            using ( var ctx = new IpDbContext( _getOptions() ) ) {
                var dbBlock = ctx.CityBlockIpv4Collection.First();
                dbBlock.CityLocation = cityLocation;
                ctx.Add( cityLocation );
                ctx.Add( cityLocation.EnCity );
                ctx.Add( cityLocation.EsCity );
                ctx.SaveChanges();
            }

            // Assert:
            using ( var ctx = new IpDbContext( _getOptions() ) ) {
                var dbBlock = ctx.CityBlockIpv4Collection.Include( c => c.CityLocation )
                                                         .ThenInclude( c => c.EnCity )
                                                         .Include( c => c.CityLocation )
                                                         .ThenInclude( c => c.EsCity )
                                                         .First();
                Assert.NotNull( dbBlock.CityLocation );
                Assert.NotNull( dbBlock.CityLocation.EnCity );
                Assert.NotNull( dbBlock.CityLocation.EsCity );
            }
        }


		#region factory
		// Insert factory methods here:

        private DbContextOptions< IpDbContext > _getOptions( [CallerMemberName]string dbName = null )
        {
            var options = new DbContextOptionsBuilder< IpDbContext >().UseInMemoryDatabase( dbName ?? "TestDb" ).Options;
            DbContextOptionsFactory.Instance.DbContextOptions = options;

            return options;
        }

		#endregion
	}

}
