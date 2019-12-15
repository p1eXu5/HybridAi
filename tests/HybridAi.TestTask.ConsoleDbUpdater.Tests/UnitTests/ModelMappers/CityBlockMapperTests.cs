using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ModelMappers
{
    [TestFixture]
	public class CityBlockMapperTests
	{
        [ OneTimeSetUp ]
        public void SetupLogger()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
        }

		[Test]
		public void BuildModelCollectionAsync_FileWithIpv4Data_FillsResult()
        {
			// Arrange:
            CityBlockMapper mapper = _getMapper();
            var fileName = _getIpv4FileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
			Assert.That( mapper.Result, Is.Not.Empty, ((TestLogger)LoggerFactory.Instance.Logger).Messages );
        }

        [Test]
		public void BuildModelCollectionAsync_FileWithIpv4Data_FillsResultWithIpv4()
        {
			// Arrange:
            CityBlockMapper mapper = _getMapper();
            var fileName = _getIpv4FileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName, 1 ).Wait();

            // Assert:
			Assert.IsTrue( mapper.Result.All( c => c is CityBlockIpv4 ) );
        }


        [Test]
        public void BuildModelCollectionAsync_FileWithIpv6Data_FillsResult()
        {
            // Arrange:
            CityBlockMapper mapper = _getMapper();
            var fileName = _getIpv6FileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.That( mapper.Result, Is.Not.Empty );
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithIpv6Data_FillsResultWithIpv6()
        {
            // Arrange:
            CityBlockMapper mapper = _getMapper();
            var fileName = _getIpv6FileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName, 1 ).Wait();

            // Assert:
            Assert.IsTrue( mapper.Result.All( c => c is CityBlockIpv6 ) );
        }


		#region factory
		// Insert factory methods here:

        private CityBlockMapper _getMapper()
        {
            return new CityBlockMapper();
        }

        private string _getIpv4FileName()
        {
            #nullable disable
            return Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv4.csv" );
            #nullable restore
        }

        private string _getIpv6FileName()
        {
#nullable disable
            return Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv6.csv" );
#nullable restore
        }

		#endregion
	}
}
