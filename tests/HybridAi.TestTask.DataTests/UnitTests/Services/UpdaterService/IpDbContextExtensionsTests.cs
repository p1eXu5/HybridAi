using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.UpdaterService;
using HybridAi.TestTask.DataTests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.DataTests.UnitTests.Services.UpdaterService
{

	[TestFixture]
	public class IpDbContextExtensionsTests
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
            var options = DbContextOptionsFactory.Instance.GetDbContextOptions();
            using var context = new IpDbContext( options );

            context.Database.EnsureDeleted();
        }


		[Test]
		public void GetEnCities__MinAndMaxGeonameIdPassed_DbContainsCities__ReturnsExpectedCount()
		{
			// Arrange:
			var dbName = "EnCitiesCount";
			var options = _getOptions( dbName );

			using ( var ctxSeed = new IpDbContext( options ) ) {
				ctxSeed.AddRange( _getEnCities() );
				ctxSeed.SaveChanges();
            }

			using var ctx = new IpDbContext( options );

			// Action:
			var cities = ctx.GetEnCities( 2, 5 ).ToArray();

			// Assert:
			Assert.IsTrue( cities.Length == 4 );
		}

		[Test]
		public void GetCityLocations__MinAndMaxGeonameIdPassed_DbContainsCityLocations__ReturnsExpectedCount()
		{
			// Arrange:
			var dbName = "CityLocationsCount";
			var options = _getOptions( dbName );

			using ( var ctxSeed = new IpDbContext( options ) ) {
				ctxSeed.AddRange( _getCityLocations() );
				ctxSeed.SaveChanges();
            }

			using var ctx = new IpDbContext( options );

			// Action:
			var cities = ctx.GetCityLocations( 2, 5 ).ToArray();

			// Assert:
			Assert.IsTrue( cities.Length == 4 );
		}

		[Test]
		public void GetCityBlockIpv4__MinAndMaxNetworkPassed_DbContainsCityBlockIpv4__ReturnsExpectedCount()
		{
			// Arrange:
			var options = DbContextOptionsFactory.Instance.GetDbContextOptions();
			var blocks = _getCityBlockIpv4s();

			using ( var ctxSeed = new IpDbContext( options ) ) {
				ctxSeed.Database.Migrate();
				ctxSeed.AddRange( blocks );
				ctxSeed.SaveChanges();
            }

			using var ctx = new IpDbContext( options );

			// Action:
			var cities = ctx.GetCityBlockIpv4s( blocks.Skip(1).First().Network, blocks.SkipLast(1).Last().Network ).ToArray();

			// Assert:
			Assert.IsTrue( cities.Length == 5, $"was {cities.Length}" );
		}

		[Test]
		public void GetCityBlockIpv6__MinAndMaxNetworkPassed_DbContainsCityBlockIpv6__ReturnsExpectedCount()
		{
			// Arrange:
			var options = DbContextOptionsFactory.Instance.GetDbContextOptions();
			var blocks = _getCityBlockIpv6s();

			using ( var ctxSeed = new IpDbContext( options ) ) {
				ctxSeed.Database.Migrate();
				ctxSeed.AddRange( blocks );
				ctxSeed.SaveChanges();
            }

			using var ctx = new IpDbContext( options );

			// Action:
			var cities = ctx.GetCityBlockIpv6s( blocks.Skip(1).First().Network, blocks.SkipLast(1).Last().Network ).ToArray();

			// Assert:
			Assert.IsTrue( cities.Length == 5, $"was {cities.Length}" );
		}


		#region factory
		// Insert factory methods here:

		private DbContextOptions< IpDbContext > _getOptions( [CallerMemberName]string dbName = null )
        {
            var options = new DbContextOptionsBuilder< IpDbContext >().UseInMemoryDatabase( dbName ?? "TestDb" ).Options;
            DbContextOptionsFactory.Instance.DbContextOptions = options;

            return options;
        }

		private List< EnCity > _getEnCities()
        {
			return new List< EnCity >() {
				new EnCity( 1 ),
				new EnCity( 2 ),
				new EnCity( 3 ),
				new EnCity( 4 ),
				new EnCity( 5 ),
				new EnCity( 6 ),
				new EnCity( 7 ),
            };
        }

		private List< CityLocation > _getCityLocations()
        {
			return new List< CityLocation >() {
				new CityLocation( 1 ),
				new CityLocation( 2 ),
				new CityLocation( 3 ),
				new CityLocation( 4 ),
				new CityLocation( 5 ),
				new CityLocation( 6 ),
				new CityLocation( 7 ),
            };
        }

		private List< CityBlockIpv4 > _getCityBlockIpv4s()
        {
			var result = new List< CityBlockIpv4 >();

			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "1.1.1.1" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "1.2.1.1" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "2.1.1.1" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "2.2.2.1" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "3.1.1.1" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "4.1.5.1" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv4( BitConverter.ToString( IPAddress.Parse( "4.2.1.1" ).GetAddressBytes() )));

            return result;
        }

		private List< CityBlockIpv6 > _getCityBlockIpv6s()
        {
			var result = new List< CityBlockIpv6 >();

			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2001:208::" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2001:240:2411:c000::" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2409:896d::" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2601:246:c081::" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2605:e000:160e::" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2a01:c50e:ed20::" ).GetAddressBytes() )));
			result.Add( new CityBlockIpv6( BitConverter.ToString( IPAddress.Parse( "2a03:cbc7::" ).GetAddressBytes() )));

            return result;
        }

		#endregion
	}

}
