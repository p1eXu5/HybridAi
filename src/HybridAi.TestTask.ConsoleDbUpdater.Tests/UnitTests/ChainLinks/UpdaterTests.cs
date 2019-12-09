using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{
    public class UpdaterTests
    {
        [Test]
		public void Process__NotImportedModelsRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
		{
            // Arrange:
            Updater mapper = _getMapper();
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
            Updater mapper = _getMapper();
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
            Updater mapper = _getMapper();
            var request = _getEmptyImportedModelsRequest();

            // Action:
            var message = ((IResponse< DoneRequest >) mapper.Process( request )).Request.Message;

            // Assert:
            StringAssert.Contains( "Imported model collections are empty.", message );
        }


        #region factory
		// Insert factory methods here:

        private Updater _getMapper()
        {
            return new Updater( null );
        }

        private ImportedModelsRequest _getEmptyImportedModelsRequest()
        {
            List< IEntity > model = new List< IEntity >() { };
            List< IEntity >[] models = new List< IEntity >[] { model };

            return new ImportedModelsRequest( models );
        }

		#endregion
    }
}
