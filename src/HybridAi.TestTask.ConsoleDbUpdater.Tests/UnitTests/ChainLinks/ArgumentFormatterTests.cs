using NUnit.Framework;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{

	[TestFixture]
	public class ArgumentFormatterTests
	{
		[TestCase( "a" )]
		[TestCase( "a.a" )]
		[TestCase( @"a\a-a.a" )]
		[TestCase( @"a:\a\a.a" )]
		[TestCase( @"a:\a\a" )]
		public void Process__PathInRequest_SuccessorIsNull__ReturnsResponseWithFileLocationRequest( string path )
		{
			// Arrange:
            var formatter = _getArgumentFormatter();
            var argumentRequest = new ArgumentRequest( path );

            // Action:
            var response = formatter.Process( argumentRequest );

            // Assert:
			Assert.IsTrue( response.Request is FileLocationRequest );
        }


        [TestCase( "http://" )]
        [TestCase( "https://" )]
        [TestCase( @"http://a/a-a.a" )]
        [TestCase( @"https://aa/a.a?sd" )]
        public void Process__PathInRequest_SuccessorIsNull__ReturnsResponseWithUrlRequest( string path )
        {
            // Arrange:
            var formatter = _getArgumentFormatter();
            var argumentRequest = new ArgumentRequest( path );

            // Action:
            var response = formatter.Process( argumentRequest );

            // Assert:
            Assert.IsTrue( response.Request is UrlRequest );
        }


		#region factory
		// Insert factory methods here:

        private IChainLink< Request, IResponse< Request > > _getArgumentFormatter()
        {
			return new ArgumentFormatter( null );
        }

		#endregion
	}

}
