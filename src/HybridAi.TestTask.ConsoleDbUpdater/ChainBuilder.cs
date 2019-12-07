using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class ChainBuilder : IDisposable
    {
        private readonly object _locker = new object();

        private List< object > _chainTypes;

        public ChainLink Result { get; private set; } = null;

        private List< object > _ChainTypes => _chainTypes ??= new List< object >(10);

        public void AddChainLink<T>( T chainLink ) where T : ChainLink
        {
            if (chainLink == null) throw new ArgumentNullException( nameof( chainLink ), @"Chain link cannot be null." ); ;

            var type = typeof(T);
            if ( !_checkConstructor(type) ) throw new ArgumentException( "Type has no properly constructor." );

            var chain = _ChainTypes;
            chain.Add( type );
        }

        public void AddChainLink<T>() where T : ChainLink
        {
            var type = typeof(T);
            if ( !_checkConstructor(type) ) throw new ArgumentException( "Type has no properly constructor." );

            var chain = _ChainTypes;
            chain.Add( type );
        }

        public ChainLink Build()
        {
            ChainLink result = null;

            var chainTypes = _ChainTypes;
            for ( int i = chainTypes.Count - 1; i >= 0; --i ) {
                switch(chainTypes[i]) {
                    case Type type:
                        result = (ChainLink)Activator.CreateInstance( type, new object[] { result } );
                        continue;
                    case ChainLink chainLink:
                        result = chainLink.SetSuccessor( result );
                        continue;
                }
            }

            lock ( _locker ) {
                Result = result;
            }

            return result;
        }


        public ChainBuilder Append( ChainBuilder builde )
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
            Reset();
        }


        private bool _checkConstructor( Type type )
        {
            bool res = false;

            foreach ( ConstructorInfo ctor in type.GetConstructors() )
            {
                ParameterInfo[] parameters = ctor.GetParameters();
                if ( parameters.Length != 1 ) continue;

                res = parameters[0].ParameterType is ChainLink;
            }

            return res;
        }
    }
}
