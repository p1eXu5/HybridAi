using System.Collections.Generic;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.Extensions
{
    [TestFixture]
    public class CityBlockExtensionsTests
    {
        [Test]
        public void RemoveAt_IndexesNotEmpty_RemovesElements()
        {
            // Arrange:
            var cities = _getCities();
            var indexes = new List<int>{ 0, 3 };

            // Action:
            cities.RemoveAtIndexes( indexes );

            // Assert:
            Assert.That( cities.Count, Is.EqualTo( 2 ) );
        }


        [Test]
        public void RemoveAt_IndexesIsEmpty_DoesNotRemoveElements()
        {
            // Arrange:
            var cities = _getCities();
            var indexes = new List<int>();

            // Action:
            cities.RemoveAtIndexes( indexes );

            // Assert:
            Assert.That( cities.Count, Is.EqualTo( 4 ) );
        }

        [Test]
        public void RemoveAt_IndexesNotContained_DoesNotRemoveElements()
        {
            // Arrange:
            var cities = _getCities();
            var indexes = new List<int>{8, 15};

            // Action:
            cities.RemoveAtIndexes( indexes );

            // Assert:
            Assert.That( cities.Count, Is.EqualTo( 4 ) );
        }

        [Test]
        public void RemoveAt_IndexesMixed_RemovesElements()
        {
            // Arrange:
            var cities = _getCities();
            var indexes = new List<int>{1, 8};

            // Action:
            cities.RemoveAtIndexes( indexes );

            // Assert:
            Assert.That( cities.Count, Is.EqualTo( 3 ) );
        }

        #region factories

        private List< City > _getCities()
        {
            return new List< City > {
                new EnCity( 0 ),
                new EnCity( 1 ),
                new EnCity( 2 ),
                new EnCity( 3 ),
            };
        }

        #endregion
    }
}
