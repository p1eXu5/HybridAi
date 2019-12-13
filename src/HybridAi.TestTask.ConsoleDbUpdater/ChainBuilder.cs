using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class ChainBuilder : IChainBuilder< IChainLink<Request, IResponse< Request >> >, IEnumerable< Type >, IDisposable
    {
        private readonly object _locker = new object();
        private IChainLink< Request, IResponse< Request > >? _result;

        private List< Type >? _chainTypes;

        #region properties

        public IChainLink<Request, IResponse<Request>> Result
        {
            get => _result ?? throw new InvalidOperationException( "Build the Result." );
            private set => _result = value;
        }

        private List< Type > _ChainTypes => _chainTypes ??= new List< Type >(10);

        #endregion


        public void AddChainLink<T>() where T : IChainLink< Request, IResponse< Request > >
        {
            var type = typeof(T);
            if ( !_checkConstructor(type) ) throw new ArgumentException( 
                $"Type must have constructor with single {nameof(IChainLink< Request, IResponse< Request > >)}." );

            var chain = _ChainTypes;
            chain.Add( type );
        }

        public IChainLink<Request, IResponse< Request >> Build()
        {
            if (_chainTypes?.Any() == true)
            {
                IChainLink<Request, IResponse< Request >>? result = null;

                var chainTypes = _ChainTypes;
                for ( int i = chainTypes.Count - 1; i >= 0; --i ) {

#nullable disable
                    result = (IChainLink<Request, IResponse< Request >>)Activator.CreateInstance( chainTypes[i], new object[] { result } );
#nullable restore
                }


                if (result != null)
                {
                    lock ( _locker ) {
                        Result = result;
                    }

                    return result;
                }
            }

            throw new InvalidOperationException( "Add chain links." );
        }

        public IChainBuilder< IChainLink< Request, IResponse< Request > > > Append( IEnumerable< Type > chainTypes )
        {
            var chains = _ChainTypes;
            chains.AddRange( chainTypes );

            return this;
        }


        public void Reset()
        {
            if ( _chainTypes?.Any() == true ) _chainTypes.Clear();
            
            if ( _result != null ) {
                lock ( _locker ) {
                    _result = null;
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

                res = parameters[0].ParameterType.FullName.Equals( typeof( IChainLink< Request, IResponse< Request > >).FullName);
            }

            return res;
        }

        public IEnumerator< Type > GetEnumerator()
        {
            var chains = _ChainTypes;
            return chains.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
