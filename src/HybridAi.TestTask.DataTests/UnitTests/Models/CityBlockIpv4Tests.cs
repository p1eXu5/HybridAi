using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;

namespace HybridAi.TestTask.DataTests.UnitTests.Models
{
    [TestFixture]
    public class CityBlockIpv4Tests
    {
        [ Test ]
        public void GetNetwork_ByDefault_ReturnsNetwork()
        {
            // Arrange:
            string cb4Network = "TestNetwork";
            var cb4 = new CityBlockIpv4( cb4Network );

            // Action:
            string network = cb4.GetNetwork();

            // Assert:
            Assert.AreEqual( cb4Network, network );
        }

        [ Test ]
        public void class_ByDefault_CannotMakeDublicatesInHashSet()
        {
            var cb1 = new CityBlockIpv4( "1.1.1.1" );
            var cb2 = new CityBlockIpv4( "1.1.1.1" );

            var set = new HashSet< CityBlockIpv4 >();
            set.Add( cb1 );
            Assert.IsFalse(set.Add( cb2 ) );

            Assert.AreEqual( 1, set.Count );
        }
    }
}
