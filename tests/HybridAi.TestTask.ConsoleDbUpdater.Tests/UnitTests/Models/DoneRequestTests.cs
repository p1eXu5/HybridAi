using HybridAi.TestTask.ConsoleDbUpdater.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.Models
{
    [TestFixture]
    public class DoneRequestTests
    {
        [Test]
        public void GetResponse_ByDefault_ReturnsResponseWithDoneRequest()
        {
            // Arrange:
            var doneReq = new DoneRequest( "test" );

            // Action:
            var response = doneReq.Response;

            // Assert:
            Assert.IsTrue( response.Request is DoneRequest );
        }
    }
}
