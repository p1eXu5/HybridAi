﻿using NUnit.Framework;
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
	public class MapperTests
	{
		[Test]
		public void Process__NotFileLocationRequest_SuccessorIsNull__ReturnsResponseWithSameRequest()
		{
            // Arrange:
            var mapper = _getMapper();
            var request = Request.EmptyRequest;

            // Action:
            var response = mapper.Process( request );

            // Assert:
            Assert.IsTrue( ReferenceEquals( response.Request, request ) );
		}

		#region factory
		// Insert factory methods here:

        private Mapper _getMapper()
        {
            return new Mapper( null );
        }

		#endregion
	}
}
