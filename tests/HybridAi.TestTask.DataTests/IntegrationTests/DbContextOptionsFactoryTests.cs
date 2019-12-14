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

        [ Test ]
        public void ctor_HasNoAppCfg_ReturnsNotNullDbOptions()
        {
            // Arrange:
            // Action:
            var factory = DbContextOptionsFactory.Instance;

            // Assert:
            Assert.NotNull( factory.GetDbContextOptions() );
        }
    }
}
