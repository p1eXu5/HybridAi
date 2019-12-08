using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.Data.Models;
using NUnit.Framework;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.UnitTests.ModelMappers
{
    [TestFixture]
    public class ModelMapperFactoryTests
    {
        [ Test ]
        public void TryFindModelMapper_HeaderExists_ReturnNotNullMapper()
        {
            // Arrange:
            ModelMapperFactory factory = _getModelMapperFactory();
            factory.Register< TestMapper >( TestMapper.TestHeader );

            // Action:
            IModelMapper< List< IEntity > >? mapper =
                factory.TryFindModelMapper( String.Join( ',', TestMapper.TestHeader ) );

            // Assert:
            Assert.NotNull( mapper );
            Assert.IsTrue( mapper is TestMapper );
        }


        #region factory

        private ModelMapperFactory _getModelMapperFactory()
        {
            return ModelMapperFactory.Instance;
        }

        #endregion


        #region fake types

        private class TestMapper : IModelMapper< List< IEntity > >
        {
            public static string[] TestHeader { get; } = new[] { "one", "two " };
            public string[] Header => TestMapper.TestHeader;
            public List< IEntity > Result { get; }
            public Task BuildModelCollectionAsync( string fileName, int offset = 0 )
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
