using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public void Process_PathInRequest_ReturnsResponseWithFileLocationRequest( string path )
		{
			// Arrange:
            var formatter = _getArgumentFormatter();
            var argumentRequest = new ArgumentRequest( path );

            // Action:
            var response = formatter.Process( argumentRequest );

            // Assert:
			Assert.IsTrue( response is IResponse< FileLocationRequest > );
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
