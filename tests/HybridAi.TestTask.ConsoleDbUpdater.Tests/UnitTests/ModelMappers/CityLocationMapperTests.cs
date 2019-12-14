using System;
using System.IO;
using System.Linq;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ModelMappers
{
    [TestFixture]
    public class CityLocationMapperTests
    {
        [ OneTimeSetUp ]
        public void SetupLogger()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithRuCityData_FillsResult()
        {
            // Arrange:
            CityLocationMapper mapper = _getMapper();
            var fileName = _getRuCityFileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.That( mapper.Result, Is.Not.Empty, ((TestLogger)LoggerFactory.Instance.Logger).Messages );
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithRuCityData_FillsResultWithCityLocation()
        {
            // Arrange:
            CityLocationMapper mapper = _getMapper();
            var fileName = _getRuCityFileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.IsTrue( mapper.Result.All( c => c is CityLocation ) );
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithRuCityData_FillsResultWithNotNullRuCity()
        {
            // Arrange:
            CityLocationMapper mapper = _getMapper();
            var fileName = _getRuCityFileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.IsTrue( mapper.Result.All( c => ((CityLocation)c).RuCity != null ) );
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithEnCityData_FillsResult()
        {
            // Arrange:
            CityLocationMapper mapper = _getMapper();
            var fileName = _getEnCityFileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.That( mapper.Result, Is.Not.Empty, ((TestLogger)LoggerFactory.Instance.Logger).Messages );
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithEnCityData_FillsResultWithCityLocation()
        {
            // Arrange:
            CityLocationMapper mapper = _getMapper();
            var fileName = _getEnCityFileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.IsTrue( mapper.Result.All( c => c is CityLocation ) );
        }

        [Test]
        public void BuildModelCollectionAsync_FileWithEnCityData_FillsResultWithNotNullEnCity()
        {
            // Arrange:
            CityLocationMapper mapper = _getMapper();
            var fileName = _getEnCityFileName();

            // Action:
            mapper.BuildModelCollectionAsync( fileName ).Wait();

            // Assert:
            Assert.IsTrue( mapper.Result.All( c => ((CityLocation)c).EnCity != null ) );
        }


        #region factory
        // Insert factory methods here:

        private CityLocationMapper _getMapper()
        {
            return new CityLocationMapper();
        }

        private string _getRuCityFileName()
        {
#nullable disable
            return Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\rucity.csv" );
#nullable restore
        }

        private string _getEnCityFileName()
        {
#nullable disable
            return Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\encity.csv" );
#nullable restore
        }

        #endregion
    }
}
