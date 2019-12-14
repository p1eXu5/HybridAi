using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.IntegrationTests.ChainLinks
{
    public class UpdaterTests
    {
        [Test]
        public void ctor__ImportedModelsRequestNotEmpty_SuccessorIsNull__CanCreateDatabase()
        {

        }


        #region factory
		// Insert factory methods here:

        private Updater _getMapper()
        {
            return new Updater( null );
        }


		#endregion
    }
}
