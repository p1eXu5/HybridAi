using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data;
using NUnit.Framework;

namespace HybridAi.TestTask.DataTests.IntegrationTests
{
    [TestFixture]
    public class DbContextOptionsFactoryTests
    {
        [ Test ]
        public void ctor_HasNoAppCfg_ReturnsConnectionString()
        {
            // Arrange:
            // Action:
            var factory = DbContextOptionsFactory.Instance;

            // Assert:
            Assert.That( factory.ConnectionString.Length, Is.GreaterThan( 10 ) );
            Assert.AreEqual( DbContextOptionsFactory.DEFAULT_CONNECTION_STRING, factory.ConnectionString );
        }
    }
}
