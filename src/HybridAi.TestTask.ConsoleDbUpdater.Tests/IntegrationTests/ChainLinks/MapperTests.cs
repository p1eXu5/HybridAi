using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.IntegrationTests.ChainLinks
{
    [TestFixture]
	public class MapperTests
    {
        #nullable disable
        private readonly string _ipv4csv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv4.csv" );

        private readonly string _ipv6csv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv6.csv" );

        private readonly string _encitycsv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\encity.csv" );

        private readonly string _rucitycsv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\rucity.csv" );

        private readonly string _wrongtxt
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\test.txt" );

        private readonly string _wrongcsv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\test.csv" );

        #nullable restore

        [ OneTimeSetUp ]
        public void SetupLogger()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
            ModelMapperFactory.Instance.Register< CityBlockMapper >( CityBlockMapper.CityBlockHeader );
            ModelMapperFactory.Instance.Register< CityLocationMapper >( CityLocationMapper.CityLocationHeader );
        }


        [Test]
        public void Process__FolderRequestContainsNotSupportedFiles_SuccessorIsNull__ReturnsResponseWithFolderRequest()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _wrongcsv, _wrongtxt );

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( response is Response< FolderRequest > );
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
        }

        [Test]
        public void Process__FolderRequestContainsSupportedFiles_SuccessorIsNull__ReturnsResponseWithImportedModelsRequest()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv4csv, _encitycsv, _rucitycsv );

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( response is Response< ImportedModelsRequest > );
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
