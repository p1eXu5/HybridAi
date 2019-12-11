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
		public void CopyCityLocationCity_CityLocationIsNull_CopyCityLocation()
		{
			// Arrange:
            var cityBlock = new CityBlock();
            var city = new EnCity( 1 ) { CityName = "Test City" };
            var cityLocation = new CityLocation( 1 ) {
                EnCity = city
            };

            // Action:
			Assert.IsNull( cityBlock.CityLocation );
			cityBlock.CopyCityLocationCity( cityLocation );

            // Assert:
			Assert.IsTrue( ReferenceEquals( cityLocation, cityBlock.CityLocation ) );
			Assert.IsTrue( ReferenceEquals( city, cityBlock.CityLocation.EnCity ) );
        }

        [Test]
		public void CopyCityLocationCity_CityLocationIsNotNull_CityIsNull__CopyCity()
		{
			// Arrange:
            var containedCity = new EnCity( 1 ) { CityName = "Test City" };
            var containedCityLocation = new CityLocation( 1 ) {
                EnCity = containedCity
            };
            var cityBlock = new CityBlock {
                CityLocation = containedCityLocation
            };

            var newCity = new EsCity( 1 ) { CityName = "EsCity" };
            var copiedCityLocation = new CityLocation( 1 ) {
                EsCity = newCity
            };

            // Action:
			cityBlock.CopyCityLocationCity( copiedCityLocation );

            // Assert:
			Assert.IsTrue( ReferenceEquals( newCity, cityBlock.CityLocation.EsCity ) );
			Assert.IsTrue( ReferenceEquals( containedCity, cityBlock.CityLocation.EnCity ) );
        }

		#region factory
		// Insert factory methods here:

		#endregion
	}

}
