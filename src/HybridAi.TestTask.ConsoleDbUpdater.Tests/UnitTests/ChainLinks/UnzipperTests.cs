using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{

	[TestFixture]
	public class UnzipperTests
	{
		[Test]
		public void Process__NotFileLocationRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
		{
            // Arrange:
            var chain = _getUnzipper();
            var request = Request.EmptyRequest;

            // Action:
            var response = chain.Process( request );

            // Assert:
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
            Assert.IsFalse( response is Response< FolderRequest > );
		}

        [Test]
        public void Process__FileLocationRequestWithEmptyZipFile_SuccessorIsNull__ReturnsResponseWithSameRequest()
        {
            // Arrange:
            var chain = _getUnzipper();
            var request = _getEmptyFileLocationRequest();

            // Action:
            var response = chain.Process( request );

            // Assert:
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
            Assert.IsFalse( response is Response< FolderRequest > );
        }

        [Test]
        public void Process__FileLocationRequestWithNotEmptyZipFile_SuccessorIsNull__ReturnsResponseWithFolderRequest()
        {
            // Arrange:
            var chain = _getUnzipper();
            var request = _getEmptyFileLocationRequest();

            // Action:
            var response = chain.Process( request );

            // Assert:
            Assert.IsFalse( ReferenceEquals( response.Request, request ) );
            Assert.IsTrue( response is Response< FolderRequest > );
        }

		#region factory
		// Insert factory methods here:
        private Unzipper _getUnzipper()
        {
            return new Unzipper( null );
        }

        private FileLocationRequest _getEmptyFileLocationRequest()
        {
            var file = Path.Combine( 
                AppDomain.CurrentDomain.BaseDirectory ?? throw new ArgumentNullException(), 
                "TestData\\empty.zip" 
            );

            if ( !File.Exists( file ) ) throw new FileNotFoundException();

            return new FileLocationRequest( file );
        }

        private FileLocationRequest _getNotEmptyFileLocationRequest()
        {
            var file = Path.Combine( 
                AppDomain.CurrentDomain.BaseDirectory ?? throw new ArgumentNullException(), 
                // ReSharper disable once StringLiteralTypo
                "TestData\\notempty.zip" 
            );

            if ( !File.Exists( file ) ) throw new FileNotFoundException();

            return new FileLocationRequest( file );
        }

		#endregion
	}

}
