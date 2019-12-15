using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Extensions;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.UpdaterService;
using HybridAi.TestTask.Data.Services.WebApi;
using HybridAi.TestTask.DataTests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.DataTests.IntegrationTests.Services.WebApi
{

	[TestFixture]
	public class IpDbContextExtensionsTests
	{
		[ OneTimeSetUp ]
        public void Setup()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
        }


		[TestCase( 0, 0, 0, 0,    1, 0, 0, 0 )]
		[TestCase( 4, 7, 9, 0,    5, 7, 9, 0 )]
		[TestCase( 255, 255, 255, 255,    255, 255, 255, 255 )]
		[TestCase( 255, 255, 254, 255,    255, 255, 255, 255 )]
		[TestCase( 254, 255, 254, 254,    255, 255, 254, 254 )]
		[TestCase( 254, 254, 254, 254,    255, 254, 254, 254 )]
		[TestCase( 254, 254, 254, 254,    255, 254, 254, 254 )]
		[TestCase( 255, 255, 255, 254,    255, 255, 255, 255 )]
		public void Increment_ByDefault_ReturnsExpected( byte ib3, byte ib2, byte ib1, byte ib0, byte ob3, byte ob2, byte ob1, byte ob0 )
        {
			// Arrange:
			var input = new byte[] { ib3, ib2, ib1, ib0 };
			var expected = new byte[] { ob3, ob2, ob1, ob0 };

            // Action:
			var transformed = input.Increment();

			// Assert:
			Assert.That( transformed, Is.EquivalentTo( expected ) );
		}

		[TestCase( 0, 0, 0, 0,    0, 0, 0, 0 )]
		[TestCase( 1, 1, 1, 1,    0, 1, 1, 1 )]
		[TestCase( 0, 1, 1, 1,    0, 0, 1, 1 )]
		[TestCase( 0, 0, 1, 1,    0, 0, 0, 1 )]
		[TestCase( 0, 0, 1, 0,    0, 0, 0, 0 )]
		[TestCase( 0, 0, 0, 1,    0, 0, 0, 0 )]
		public void Decrement_ByDefault_ReturnsExpected( byte ib3, byte ib2, byte ib1, byte ib0, byte ob3, byte ob2, byte ob1, byte ob0 )
        {
			// Arrange:
			var input = new byte[] { ib3, ib2, ib1, ib0 };
			var expected = new byte[] { ob3, ob2, ob1, ob0 };

            // Action:
			var transformed = input.Decrement();

			// Assert:
			Assert.That( transformed, Is.EquivalentTo( expected ) );
		}


		[TestCase( "DF-FF-F4-12", "DF-FF-F4-00" )]
		public void GetCityBlockIp4( string inIp, string outIp)
		{
			// Arrange:
			var inBytes = inIp.ToIp();
			using var ctx = new IpDbContext(_getNpgsqlDbContextOptions());
			// Action:
			CityBlockIpv4 ip4 = ctx.GetCityBlockIpv4(inBytes).Result;

			// Assert:
			var resIp = ip4.Network;
			Assert.AreEqual(outIp, resIp);
		}

		[TestCase( "20-01-02-40-28-D1-00-00-00-00-00-00-00-00-00-00", "20-01-02-40-28-D0-00-00-00-00-00-00-00-00-00-00" )]
		public void GetCityBlockIp6( string inIp, string outIp)
		{
			// Arrange:
			var inBytes = inIp.ToIp();
			using var ctx = new IpDbContext(_getNpgsqlDbContextOptions());
			// Action:
			CityBlockIpv6 ip6 = ctx.GetCityBlockIpv6(inBytes).Result;

			// Assert:
			var resIp = ip6.Network;
			Assert.AreEqual(outIp, resIp);
		}

		#region factory
		// Insert factory methods here:

		private DbContextOptions< IpDbContext > _getNpgsqlDbContextOptions()
        {
            var options = DbContextOptionsFactory.Instance.GetDbContextOptions();
            using (var context = new IpDbContext(options))
            {
                context.Database.Migrate();
            }

            return options;
        }

		#endregion
	}

}
