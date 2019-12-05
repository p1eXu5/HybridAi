using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class ArgumentFormaterChainLink : IChainLink
    {
        private readonly IChainLink _successor;

        public ArgumentFormaterChainLink( IChainLink successor )
        {
            _successor = successor;
        }

        public Request Process( Request request )
        {
            return _successor?.Process( request );
        }
    }
}
