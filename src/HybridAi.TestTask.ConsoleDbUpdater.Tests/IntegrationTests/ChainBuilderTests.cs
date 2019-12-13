using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
// ReSharper disable ClassNeverInstantiated.Local

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.IntegrationTests
{

	[TestFixture]
	public class ChainBuilderTests
	{
        private static List< Type > _chainNames;

        [SetUp]
        public void PrepareChainNames()
        {
            _chainNames = new List< Type >();
        }

		[Test]
		public void AddChainLinks__ChainLinkTypeWithoutSupportedCtor_DerivedFromChainLink__ThrowsArgumentException()
		{
			var builder = new ChainBuilder();

            var ex = Assert.Catch< ArgumentException >( () => builder.AddChainLink< NotSupportedDerivedChain >() );

			StringAssert.Contains( $"Type must have constructor with single {nameof(IChainLink< Request, IResponse< Request > >)}.", ex.Message );
        }

        [Test]
        public void AddChainLinks__ChainLinkTypeWithoutSupportedCtor_IsNotDerivedFromChainLink__ThrowsArgumentException()
        {
            var builder = new ChainBuilder();

            var ex = Assert.Catch< ArgumentException >( () => builder.AddChainLink< NotSupportedChain >() );

            StringAssert.Contains( $"Type must have constructor with single {nameof(IChainLink< Request, IResponse< Request > >)}.", ex.Message );
        }

        [Test]
        public void AddChainLinks__ChainLinkTypeDerived_FromChainLink__DoesNotThrowArgumentException()
        {
            var builder = new ChainBuilder();

            Assert.DoesNotThrow( () => builder.AddChainLink< SupportedChain >() );
        }


        [ Test ]
        public void Result_ByDefault_ThrowsInvalidOperationException()
        {
            var builder = new ChainBuilder();

            var ex = Assert.Catch< InvalidOperationException >( () => { var _ = builder.Result; } );

            StringAssert.Contains( $"Build the Result", ex.Message );
        }



        [ Test ]
        public void Build_HaveChainLinks_ReturnsNotNull()
        {
            // Arrange:
            var builder = new ChainBuilder();
            builder.AddChainLink< ChainOne >();

            // Action:
            builder.Build();

            // Assert:
            Assert.NotNull( builder.Result );
        }

        [ Test ]
        public void Build_HaveTwoChainLinks_ReturnsResultFirstAddedChainLink()
        {
            // Arrange:
            var builder = new ChainBuilder();
            builder.AddChainLink< ChainOne >();
            builder.AddChainLink< SupportedChain >();

            // Action:
            var res = builder.Build();

            // Assert:
            Assert.IsTrue( res is ChainOne );
        }

        [ Test ]
        public void Build_ByDefault_ThrowsInvalidOperationException()
        {
            var builder = new ChainBuilder();

            var ex = Assert.Catch< InvalidOperationException >( () => { var _ = builder.Build() ; } );

            StringAssert.Contains( $"Add chain links.", ex.Message );
        }


        [ Test ]
        public void Build_HaveChainLinks_CreatesChainl()
        {
            // Arrange:
            var builder = new ChainBuilder();
            builder.AddChainLink< ChainOne >();
            builder.AddChainLink< ChainTwo >();
            var expected = new Type[] { typeof( ChainOne ), typeof( ChainTwo ) };

            // Action:
            builder.Build().Process( Request.EmptyRequest );

            // Assert:
            Assert.That( _chainNames, Is.EquivalentTo( expected ) );
        }

		#region factory
		// Insert factory methods here:

		#endregion


        #region fakes

        private class NotSupportedDerivedChain : ChainLink
        {
            public NotSupportedDerivedChain( IChainLink< Request, IResponse< Request > > successor, int additionalParameter ) : base( successor )
            { }
        }

        private class NotSupportedChain : IChainLink< Request, IResponse< Request > >
        {
            public NotSupportedChain( int additionalParameter )
            { }

            public IChainLink< Request, IResponse< Request > > Successor { get; }
            public IResponse< Request > Process( Request request )
            {
                throw new NotImplementedException();
            }
        }

        private class SupportedChain : ChainLink
        {
            public SupportedChain( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
            { }
        }

        private class ChainOne : ChainLink
        {
            public ChainOne( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
            { }

            public override IResponse< Request > Process( Request request )
            {
                _chainNames.Add( typeof( ChainOne ) );
                return base.Process( request );
            }
        }

        private class ChainTwo : ChainLink
        {
            public ChainTwo( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
            { }

            public override IResponse< Request > Process( Request request )
            {
                _chainNames.Add( typeof( ChainTwo ) );
                return base.Process( request );
            }
        }

        #endregion
	}

}
