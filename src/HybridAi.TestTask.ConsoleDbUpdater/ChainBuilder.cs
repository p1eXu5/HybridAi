﻿using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class ChainBuilder : IChainBuilder< IChainLink<Request, IResponse< Request >> >, IDisposable
    {
        private readonly object _locker = new object();
        private IChainLink< Request, IResponse< Request > >? _result;

        private List< object >? _chainTypes;

        public IChainLink< Request, IResponse< Request > > Result
        {
            get => _result ?? throw new InvalidOperationException(); 
            private set => _result = value;
        }

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

        public IChainLink<Request, IResponse< Request >> Build()
        {
            if (_chainTypes?.Any() == true)
            {
                IChainLink<Request, IResponse< Request >>? result = null;

                var chainTypes = _ChainTypes;
                for ( int i = chainTypes.Count - 1; i >= 0; --i ) {
                    switch(chainTypes[i]) {
                        case Type type:
#pragma warning disable CS8601 // Possible null reference assignment.
                            result = (IChainLink<Request, IResponse< Request >>?)Activator.CreateInstance( type, new object[] { result } );
#pragma warning restore CS8601 // Possible null reference assignment.
                            continue;
                        case ChainLink chainLink:
                            result = chainLink.SetSuccessor( result );
                            continue;
                    }
                }


                if (result != null)
                {
                    lock ( _locker ) {
                        Result = result;
                    }

                    return result;
                }
            }

            throw new InvalidOperationException();
        }


        public IChainBuilder< IChainLink<Request, IResponse< Request > > > Append( IEnumerable< object > chines )
        {
            throw new NotImplementedException();
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

                res = parameters[0].ParameterType is ChainLink;
            }

            return res;
        }

        public IEnumerator< object > GetEnumerator()
        {
            return _ChainTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
