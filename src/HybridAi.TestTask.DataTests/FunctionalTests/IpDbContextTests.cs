using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HybridAi.TestTask.DataTests.FunctionalTests
{
    [ TestFixture ]
    public class IpDbContextTests
    {
        [ TearDown ]
        public void DeleteDatabase()
        {
            using var context = new IpDbContext();

            context.Database.EnsureDeleted();
        }

        [ Test ]
        public void class_ByDefault_CanCreateDatabase()
        {
            using var context = new IpDbContext();

            context.Database.Migrate();
        }

        #region factories

        #endregion
    }
}
