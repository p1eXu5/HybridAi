using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using Moq;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ChainLinks
{

	[TestFixture]
	public class ChainLinkTests
    {
        private Mock< IChainLink< Request, Response > >? _mockSuccessor;

        [Test]
        public void Process_SuccessorIsNull_ReturnsNotNullResponse()
        {
            // Arrange:
            var chain = _getChainLink();

            // Action:
            var response = chain.Process( new Request() );

            // Assert:
            Assert.NotNull( response );
        }

        [Test]
        public void Process_SuccessorIsNotNull_CallsSuccessorProcess()
        {
            // Arrange:
            var chain = _getMockedChainLink();

            // Action:
            var response = chain.Process( new Request() );

            // Assert:
            _mockSuccessor.Verify( s => s.Process( It.IsAny<Request>() ), Times.Once );
        }

		#region factory
		// Insert factory methods here:

        private ChainLink _getChainLink()
        {
            return new FakeChainLink( null );
        }

        private ChainLink _getMockedChainLink()
        {
            _mockSuccessor = new Mock<IChainLink<Request,Response>>();

            return new FakeChainLink( _mockSuccessor.Object );
        }

		#endregion

		#region fakes

        private class FakeChainLink : ChainLink
        {
            public FakeChainLink( IChainLink< Request, Response >? successor ) 
                : base( successor )
            {
            }
        }

		#endregion
	}

}
