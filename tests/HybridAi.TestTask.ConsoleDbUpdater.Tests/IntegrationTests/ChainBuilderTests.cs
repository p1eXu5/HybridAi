using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers;
using HybridAi.TestTask.Data;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.IntegrationTests
{

	[TestFixture]
	public class ChainBuilderTests
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


		[ OneTimeSetUp ]
        public void Setup()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
            DbContextOptionsFactory.Instance.ConnectionString =
                "Host=localhost;Database=TestIpDb;Username=testuser;Password=123;IntegratedSecurity=true";
        }

        [ TearDown ]
        public void DeleteDatabase()
        {
            using var context = new IpDbContext();

            context.Database.EnsureDeleted();
        }

		[Test]
		public void Process_FolderWithTestFilesWithData_AddRecordsToDatabase()
		{
            FolderRequest request = _getFolderRequest( _ipv4csv, _ipv6csv, _encitycsv, _rucitycsv );

            // Action:
            //var response =  chain.Process( request );

            //// Assert:
            //Assert.IsTrue( response.Request is DoneRequest );
            //Assert.IsTrue( String.IsNullOrWhiteSpace( LoggerFactory.Instance.Logger.GetMessages() ) );
		}

		#region factory
		// Insert factory methods here:
        private FolderRequest _getFolderRequest( params string[] files )
        {
            return new FolderRequest( files );
        }
		#endregion
	}

}
