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

        [Test]
		public void Process__NotImportedModelsRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
		{
            // Arrange:
            Updater mapper = _getUpdater();
            var request = Request.EmptyRequest;

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
		}

        [Test]
        public void ctor__ImportedModelsRequestIsEmpty_SuccessorIsNull__ReturnsResponseWithDoneRequest()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsFalse( ReferenceEquals( response.Request, request ) );
            Assert.IsTrue( response is Response< DoneRequest > );
        }

        [Test]
        public void ctor__ImportedModelsRequestIsEmpty_SuccessorIsNull__ReturnsResponseWithRequestWithExpectedMessage()
        {
            // Arrange:
            Updater mapper = _getUpdater();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var message = ((IResponse< DoneRequest >) mapper.Process( request )).Request.Message;

            // Assert:
            StringAssert.Contains( "Imported model collections are empty.", message );
        }

        [ Test ]
        public void Process__RequestContainsCityBlockIpv6Data_DbIsEmpty__CanAddDataToDatabase()
        {
            // Arrange:
            var updater = _getUpdater();
            //var request = _getImportedModelsRequestWithIpv6();

            // Action:
            //updater.Process( request );
            //updater.Dispose();

            // Assert:
            using var ctx = new IpDbContext( DbContextOptionsFactory.Instance.DbContextOptions );
            var ipv6 = ctx.CityBlockIpv6Collection.ToArray();

            Assert.That( ipv6, Is.Not.Empty );
        }


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

		#endregion
    }
}
