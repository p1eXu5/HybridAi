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
        public void ctor__ImportedModelsRequestIsEmpty_SuccessorIsNull__ReturnsResponseWithDoneRequest()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var response = mapper.Process(request);

            // Assert:
            Assert.IsFalse(ReferenceEquals(response.Request, request));
            Assert.IsTrue(response is Response<DoneRequest>);
        }

        [Test]
        public void ctor__ImportedModelsRequestIsEmpty_SuccessorIsNull__ReturnsResponseWithRequestWithExpectedMessage()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var message = ((IResponse<DoneRequest>)mapper.Process(request)).Request.Message;

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
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbIsEmpty__AddsDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, _) = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());

        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksAndCityLocations_DbIsEmpty__ReturnsDoneWithNewCount()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, _) = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksOnly_DbIsEmpty__AddsDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, _) = _getCityBlocks();

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
            (ImportedModelsRequest request, _) = _getCityBlocks();

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
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocksAndCityLocations();

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
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksOnly_DbHasData__UpdatesDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocks();

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
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocks();

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
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocksAndCityLocations();

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
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocksAndCityLocations();

            // Action:
            using (var updater = _getUpdater())
            {

            }

            // Assert:
            using var ctx = new IpDbContext(_getOptions());
        }

        [Test]
        [Ignore("Not implemented.")]
        public void Process__ImportedModelsAreCityBlocksOnly_DbHasSomeData__AddsAndUpdatesDataToDatabase()
        {
            // Arrange:
            DbContextOptionsFactory.Instance.DbContextOptions = _getOptions();
            (ImportedModelsRequest request, ForDbSeedRecords dbRecords) = _getCityBlocks();

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

        private (ImportedModelsRequest request, ForDbSeedRecords dbRecords ) _getCityBlocksAndCityLocations()
        {
            throw new NotImplementedException();
        }

        private (ImportedModelsRequest request, ForDbSeedRecords dbRecords ) _getCityBlocks()
        {
            throw new NotImplementedException();
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
