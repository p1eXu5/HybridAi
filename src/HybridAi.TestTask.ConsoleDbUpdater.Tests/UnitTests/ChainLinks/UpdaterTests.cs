using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Services.UpdaterService;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{
    public class UpdaterTests
    {
        [ OneTimeSetUp ]
        public void Setup()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
        }


        #region ctor tests

        [Test]
        public void ctor__ImportedModelsRequestIsEmpty_SuccessorIsNull__ReturnsResponseWithFailRequest()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var response = mapper.Process(request);

            // Assert:
            Assert.IsFalse( ReferenceEquals(response.Request, request) );
            Assert.IsTrue( response.Request is FailRequest );
        }

        [Test]
        public void ctor__ImportedModelsRequestIsEmpty_SuccessorIsNull__ReturnsResponseWithRequestWithExpectedMessage()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var message = ((FailRequest)mapper.Process(request).Request).Message;

            // Assert:
            StringAssert.Contains("Imported model collections are empty.", message);
        }

        #endregion


        #region Process tests

        [Test]
        public void Process__NotImportedModelsRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = Request.EmptyRequest;

            // Action:
            var response = mapper.Process(request);

            // Assert:
            Assert.IsTrue(ReferenceEquals(response.Request, request));
        }



        [Test]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbIsEmpty__AddsDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            ImportedModelsRequest request = _getCityBlocksAndCityLocations();

            // Action:
            using (Updater updater = _getUpdater())
            {
                updater.Process( request );
            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

            var blocksIpv4 = ctx.GetIpv4Blocks().ToArray();
            var blocksIpv6 = ctx.GetIpv6Blocks().ToArray();
            var cityLocations = ctx.GetCityLocations().ToArray();
            var enCities = ctx.GetEnCities().ToArray();
            var esCities = ctx.GetEsCities().ToArray();

            Assert.That( blocksIpv4.Length, Is.EqualTo( 2 ), LoggerFactory.Instance.Logger.GetMessages() );
            Assert.That( blocksIpv6.Length, Is.EqualTo( 2 ), LoggerFactory.Instance.Logger.GetMessages() );
            Assert.That( cityLocations.Length, Is.EqualTo( 2 ), LoggerFactory.Instance.Logger.GetMessages() );
            Assert.That( enCities.Length, Is.EqualTo( 2 ), LoggerFactory.Instance.Logger.GetMessages() );
            Assert.That( esCities.Length, Is.EqualTo( 1 ), LoggerFactory.Instance.Logger.GetMessages() );
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbIsEmpty__ReturnsDoneWithNewCount()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            ImportedModelsRequest request = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityLocations_DbIsEmpty__AddsDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, _) = _getCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

        }



        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbHasData__UpdatesDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            ImportedModelsRequest request = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbIsEmpty__ReturnsDoneWithUpdateCount()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            ImportedModelsRequest request = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityLocations_DbHasData__UpdatesDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

        }



        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbHasSomeData__AddsAndUpdatesDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            ImportedModelsRequest request = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbIsEmpty__ReturnsDoneWithNewCountAndUpdateCount()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            ImportedModelsRequest request = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityLocations_DbHasSomeData__AddsAndUpdatesDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

        }

        #endregion


        #region factory
        // Insert factory methods here:

        private Updater _getUpdater()
        {
            return new Updater( null );
        }

        private ImportedModelsRequest _getEmptyImportedModelsRequest()
        {
            List< IEntity > model = new List< IEntity >() { };
            List< IEntity >[] models = new List< IEntity >[] { model };

            return new ImportedModelsRequest( models );
        }

        private DbContextOptions< IpDbContext > _getOptions( [CallerMemberName]string dbName = null )
        {
            var options = new DbContextOptionsBuilder< IpDbContext >().UseInMemoryDatabase( dbName ?? "TestDb" ).Options;
            DbContextOptionsFactory.Instance.DbContextOptions = options;

            return options;
        }

        private ImportedModelsRequest _getCityBlocksAndCityLocations()
        {
            return 
                new ImportedModelsRequest( 
                     new List< IEntity >[] {
                         new List< IEntity > {
                             new CityBlockIpv4( "1.1.1.1" ) {
                                 CityLocationGeonameId = 1,
                             },
                             new CityBlockIpv4( "2.2.2.2" ) {
                                 CityLocationGeonameId = 2,
                             }
                         },
                         new List< IEntity > {
                             new CityBlockIpv6( "11:11::11:11" ) {
                                 CityLocationGeonameId = 1,
                             },
                             new CityBlockIpv6( "22:22::22:22" ) {
                                 CityLocationGeonameId = 2,
                             }
                         },
                         new List< IEntity > {
                             new CityLocation( 1 ) {
                                 ContinentCode = "AA",
                                 CountryIsoCode = "AA",
                                 TimeZone = "AA",
                                 EnCity = new EnCity( 1 ) {
                                     LocaleCode = new LocaleCode( "en" )
                                 }
                             },
                             new CityLocation( 2 ) {
                                 ContinentCode = "BB",
                                 CountryIsoCode = "BB",
                                 TimeZone = "BB",
                                 EnCity = new EnCity( 2 ) {
                                     LocaleCode = new LocaleCode( "en" )
                                 }
                             }
                         },
                         new List< IEntity > {
                             new CityLocation( 1 ) {
                                 ContinentCode = "AA",
                                 CountryIsoCode = "AA",
                                 TimeZone = "AA",
                                 EsCity = new EsCity( 1 ) {
                                     LocaleCode = new LocaleCode( "es" )
                                 }
                             },
                         },
                     } );
        }

        private (ImportedModelsRequest request, ForDbSeedRecords dbRecords ) _getCityLocations()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region test types

        private struct ForDbSeedRecords
        {
            public IEnumerable< CityBlockIpv4 > CityBlockIpv4s;
            public IEnumerable< CityBlockIpv6 > CityBlockIpv6s;
            public IEnumerable< CityLocation > CityLocations;
        }

        #endregion
    }
}
