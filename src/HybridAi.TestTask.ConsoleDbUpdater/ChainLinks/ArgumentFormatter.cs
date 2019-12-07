using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class ArgumentFormatter : ChainLink
    {
        private readonly char[] _urlSymbols = new[] { '?' };

        public ArgumentFormatter( ChainLink? successor ) 
            : base( successor )
        { }

        public override Request Process( Request request )
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

                return base.Process( new FileLocationResponse( a ) );
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
