/*
 * Unit tests for Downloader class.
 * ================================
 *
 */

using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using NUnit.Framework;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{
    [TestFixture]
    public class DownloaderTests
    {
        [Test]
        public void Process__NotUrlRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
        {
            // Arrange:
            var chain = _getDownloader();
            var request = Request.EmptyRequest;

            // Action:
            var response = chain.Process( request );

            // Assert:
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
            Assert.IsFalse( response.Request is FileLocationRequest );
        }

        [Test]
        public void Process__UrlIsNotExisted_SuccessorIsNull__ReturnsResponseWithUrlRequest()
        {
            // Arrange:
            var chain = _getDownloader();

            // Action:
            var response = chain.Process( new UrlRequest( "https://a.a/a.zip" ) );

            // Assert:
            Assert.IsTrue( response.Request is UrlRequest );
        }

        #region factory
        // Insert factory methods here:
        private Downloader _getDownloader()
        {
            return new Downloader( null );
        }

        #endregion
    }
}
