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
        #region fields

#nullable disable

        private readonly string _ipv4csv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv4.csv" );

        private readonly string _ipv6csv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\ipv6.csv" );

        private readonly string _encitycsv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\encity.csv" );

        private readonly string _encity1csv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\encity1.csv" );

        private readonly string _rucitycsv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\rucity.csv" );

        private readonly string _wrongtxt
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\test.txt" );

        private readonly string _wrongcsv
            = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "TestData\\test.csv" );

#nullable restore

        #endregion


        [OneTimeSetUp ]
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
            Assert.IsTrue( response is Response< Request > );
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
        }

        [Test]
        public void Process__FolderRequestContainsSupportedFiles_SuccessorIsNull__ReturnsResponseWithImportedModelsRequest()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv6csv, _encitycsv, _rucitycsv );

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( response.Request is ImportedModelsRequest );
        }

        [Test]
        public void Process__FolderRequestContainsSupportedFiles_SuccessorIsNull__ReturnsExpectedCollectionCount()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv6csv, _encitycsv, _rucitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;

            // Assert:
            Assert.AreEqual( 4, collections.Length );
        }

        [Test]
        public void Process__FolderRequestContainsCityBlockIpv4Files_SuccessorIsNull__ReturnsExpectedCityBlockIpv4Count()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv6csv, _encitycsv, _rucitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;
            var ipv4Collections = collections.Where( c => c[0] is CityBlockIpv4 ).ToArray();
            // Assert:
            Assert.AreEqual( 6, ipv4Collections.Sum( ipv4 => ipv4.Count ) );
        }

        [Test]
        public void Process__FolderRequestContainsCityBlockIpv4Files_SuccessorIsNull__ReturnsCityBlockIpv4WithCityLocationIds()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv6csv, _encitycsv, _rucitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;
            var ipv4Collections = collections.Where( c => c[0] is CityBlockIpv4 ).ToArray();
            // Assert:
            Assert.IsTrue( ipv4Collections.All( ipv4 => ipv4.All( b => {
                var cl = b as CityBlock;
                return cl?.CityLocationGeonameId != null || cl?.RegistredCountryGeonameId != null;
            } ) ) );
        }

        [Test]
        public void Process__FolderRequestContainsCityBlockIpv6Files_SuccessorIsNull__ReturnsExpectedCityBlockIpv6Count()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv6csv, _encitycsv, _rucitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;
            var ipv6Collections = collections.Where( c => c[0] is CityBlockIpv6 ).ToArray();
            // Assert:
            Assert.AreEqual( 7, ipv6Collections.Sum( ipv6 => ipv6.Count ) );
        }

        [Test]
        public void Process__FolderRequestContainsEnCityFiles_SuccessorIsNull__ReturnsExpectedEnCityCount()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _encitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;
            var cityLocationCollections = collections.Where( c => c[0] is CityLocation ).ToArray();
            // Assert:
            Assert.AreEqual( 9, cityLocationCollections.Select( c => c.Select( e => ((CityLocation)e).EnCity) )
                                                       .Aggregate( new List<City>(), (a, c) => { 
                                                           a.AddRange(c);
                                                           return a;
                                                       } )
                                                       .Count, ((TestLogger)LoggerFactory.Instance.Logger).Messages );
        }

        [Test]
        public void Process__FolderRequestContainsEnCityFiles_SuccessorIsNull__ReturnsEnCityWithNotNullLocaleCodeName()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _encitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;
            var cityLocationCollections = collections.Where( c => c[0] is CityLocation ).ToArray();
            // Assert:
            Assert.IsTrue( cityLocationCollections.Select( c => c.Select( e => ((CityLocation)e).EnCity) )
                                                       .Aggregate( new List<City>(), (a, c) => { 
                                                           a.AddRange(c);
                                                           return a;
                                                       } )
                                                       .All( c => !String.IsNullOrWhiteSpace( c.LocaleCodeName ) ) );
        }

        [Test]
        public void Process__FolderRequestContainsRuCityFiles_SuccessorIsNull__ReturnsExpectedRuCityCount()
        {
            // Arrange:
            var mapper = _getMapper();
            FolderRequest request = _getFolderRequest( _rucitycsv );

            // Action:
            var collections = ((ImportedModelsRequest)mapper.Process( request ).Request).ModelCollections;
            var cityLocationCollections = collections.Where( c => c[0] is CityLocation ).ToArray();
            
            // Assert:
            var cities = cityLocationCollections.Select( c => c.Select( e => ((CityLocation)e).RuCity) )
                                                .Aggregate( 
                                                    new List<City>(), 
                                                    (a, c) => { 
                                                        a.AddRange(c);
                                                        return a;
                                                    } );

            Assert.AreEqual( 9, cities.Count, ((TestLogger)LoggerFactory.Instance.Logger).Messages );
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
