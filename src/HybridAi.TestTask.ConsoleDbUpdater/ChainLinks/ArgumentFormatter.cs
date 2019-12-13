﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class ArgumentFormatter : ChainLink
    {
        private readonly char[] _urlSymbols = new[] { '?' };

        public ArgumentFormatter( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }


        public override IResponse< Request > Process( Request request )
        {
            if (request is ArgumentRequest arg)
            {
                string a = arg.Argument.Trim().ToLowerInvariant();
                
                if ( a.StartsWith( @"https://" ) 
                     || a.StartsWith( @"http://" )
                     || _containsSymbols( a ) )
                {
                    return base.Process( new UrlRequest( a ) );
                }

                return base.Process( new FileLocationRequest( a ) );
            }
            
            return base.Process( request );
        }

        private bool _containsSymbols( string s )
        {
            foreach (char c in s)
            {
                if ( _urlSymbols.Contains( c ) ) return true;
            }

            return false;
        }

    }
}
