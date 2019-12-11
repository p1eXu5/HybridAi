using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.Extensions
{

	[TestFixture]
	public class ChainBuilderExtensionsTests
	{
		[Test]
		public void CopyCity_CityNotNull_DoesNotCopy()
		{
			// Arrange:
            var cb = new CityBlock();
            var city = new EnCity( 1 ) { CityName = "Test City" };
            var cityLocation = new CityLocation( 1 ) {
                EnCity = city
            };

            // Action:
			Assert.IsNull( cb.CityLocation );
			cb.CopyCityLocationCity( cityLocation );

            // Assert:
			Assert.IsTrue( ReferenceEquals( cityLocation, cb.CityLocation ) );
			Assert.IsTrue( ReferenceEquals( city, cb.CityLocation.EnCity ) );
        }

		#region factory
		// Insert factory methods here:

		#endregion
	}

}
