using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework.Internal;
// ReSharper disable InconsistentNaming

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{
    [TestFixture]
	public class MapperTests
    {
        #nullable disable
        private readonly string _ipv4csv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv4.csv" );
        #nullable restore

		[Test]
		public void Process__NotFolderRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
		{
            // Arrange:
            var mapper = _getMapper();
            var request = Request.EmptyRequest;

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
		}


        [Test]
        public void Process__FolderRequestContainsCityBlockIpv4File_SuccessorIsNull__ReturnsResponseWithCityBlockIpv4Models()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv );

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( response is Response< ModelRequest< CityBlock > > );
        }



		#region factory
		// Insert factory methods here:

        private Mapper _getMapper()
        {
            return new Mapper( null );
        }

        private FolderRequest _getFolderRequest( params string[] files )
        {
            return new FolderRequest( files );
        }

		#endregion
	}
}
