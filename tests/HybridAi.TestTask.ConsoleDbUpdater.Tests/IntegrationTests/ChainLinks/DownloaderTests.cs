/*
 * Integration tests for Downloader class.
 * =======================================
 *
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.IntegrationTests.ChainLinks
{

	[TestFixture]
	public class DownloaderTests
	{
        [OneTimeSetUp ]
        public void SetupLogger()
        {
            LoggerFactory.Instance.Logger = new TestLogger();
        }

		[Test]
		public void Process__DEFAULT_FILE_URL__DownloadsFileIntoInternetCacheFolder()
        {
			// Arrange:
            var downloader = _getDownloader();
            const string url = Program.DEFAULT_FILE_URL;
            var urlRequest = new UrlRequest( url );

            // Action:
            var response = downloader.Process( urlRequest );

            // Assert:
            Assert.IsTrue( File.Exists( (response.Request as FileLocationRequest)?.Path ?? "" ), LoggerFactory.Instance.Logger.GetMessages() );
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
