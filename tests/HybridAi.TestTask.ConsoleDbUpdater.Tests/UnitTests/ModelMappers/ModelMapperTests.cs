using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ModelMappers
{

	[TestFixture]
	public class ModelMapperTests
	{

        [TestCase("343137,en,AF,Africa,ET,Ethiopia,SN,\"Southern Nations, Nationalities, and People's Region\",,,Awasa,,Africa/Addis_Ababa,0", 14)]
        [TestCase("\"343137,en,213\"", 1)]
        [TestCase("343\"137,en,213\"", 1)]
        [TestCase("343\"137,en,213\", sdff\"", 2)]
        [TestCase("343\"137,en,213\", \"sdf", 2)]
        [TestCase("df4, 343\"137,en,213\"", 2)]
        [TestCase("\"df4, 343\"137,en,213\"", 3)]
        [TestCase("df4\", 343\"137,en,213\"", 3)]
        [TestCase("\"df4\", 343\"137,en,213\"", 2)]
        [TestCase("\"df4\", \"343\"137,\"en\",\"213\"", 4)]
        [TestCase("343\"137,en,213\", \"sdff\"", 2)]
        public void _Split_LineContainsValueInParentheses_ReturnsCorrectValueArray( string line, int count )
        {
            // Arrange:
            var mapper = _getFakeMapper();

            // Action:
            string[] res = mapper.Split( line );

            // Assert:
            Assert.IsTrue( res.Length == count );
        }

		#region factory
		// Insert factory methods here:

        private FakeCityLocationMapper _getFakeMapper()
        {
            return new FakeCityLocationMapper();
        }

		#endregion

        #region fakes

        private class FakeCityLocationMapper : CityLocationMapper
        {
            public string[] Split( string line )
            {
                return _Split( line.Split( _Splitters ) );
            }
        }

        #endregion
	}

}
