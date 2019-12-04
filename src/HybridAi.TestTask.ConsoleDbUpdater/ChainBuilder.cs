using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class ChainBuilder : IDisposable
    {
        private object _locker = new object();

        private List< Type > _chainTypes;

        public IChainLink Result { get; private set; } = null;

        private List< Type > _ChainTypes => _chainTypes ??= new List< Type >(10);

        public void AddChainLink<T>( T chainLink ) where T : IChainLink
        {
            if (chainLink == null) throw new ArgumentNullException( nameof( chainLink ), @"Chain link cannot be null." ); ;

            var type = typeof(T);
            if ( !_checkConstructor(type) ) throw new ArgumentException( "Type has no properly constructor." );

            var chain = _ChainTypes;
            chain.Add( type );
        }

        public IChainLink Build()
        {
            IChainLink result = null;

            var chainTypes = _ChainTypes;
            for ( int i = chainTypes.Count - 1; i >= 0; --i ) {
                result = (IChainLink)Activator.CreateInstance( chainTypes[i], new object[] { result } );
            }

            lock ( _locker ) {
                Result = result;
            }

            return result;
        }

        public void Dispose()
        {
            Reset();
        }

        public void Reset()
        {
            if ( _chainTypes?.Any() == true ) _chainTypes.Clear();
            
            if ( Result != null ) {
                lock ( _locker ) {
                     Result = null;
                }
            }
        }

        private bool _checkConstructor( Type type )
        {
            bool res = false;

            foreach ( ConstructorInfo ctor in type.GetConstructors() )
            {
                ParameterInfo[] parameters = ctor.GetParameters();
                if ( parameters.Length != 1 ) continue;

                res = parameters[0].ParameterType is IChainLink;
            }

            return res;
        }
    }
}
