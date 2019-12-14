using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Comparators;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.DataTests.UnitTests.Comparators
{

	[TestFixture]
	public class LocaleCodeComparerTests
	{
		[Test]
		public void Compare_EqualsLocaleCodes_ReturnTrue()
		{
			// Arrange:
			var lcA = new LocaleCode( "en" );
			var lcB = new LocaleCode( "en" );

			// Action:
			var res = new LocaleCodeComparer().Equals( lcA, lcB );

			// Assert:
			Assert.IsTrue( res );
		}


		[Test]
		public void Distinct_EqualsLocaleCodes_ReturnOneObject()
		{
			// Arrange:
			var lcA = new LocaleCode( "en" );
			var lcB = new LocaleCode( "en" );
			var list = new List< LocaleCode >() { lcA, lcB };

			// Action:
			var res = list.Distinct( new LocaleCodeComparer()).ToList();

			// Assert:
			Assert.IsTrue( res.Count == 1 );
		}

		#region factory
		// Insert factory methods here:

		#endregion
	}

}
