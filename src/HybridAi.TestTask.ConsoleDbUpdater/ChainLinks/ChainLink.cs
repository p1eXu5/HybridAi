using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public abstract class ChainLink : IChainLink< Request, Response >
    {
        private ChainLink _successor;

        protected ChainLink( ChainLink successor )
        {
            _successor = successor;
        }

        public virtual Response Process( Request request )
        {
            return _successor?.Process( request );
        }

        public ChainLink SetSuccessor( ChainLink successor )
        {
            _successor = successor ?? throw new ArgumentNullException(nameof(successor), @"Successor cannot be null.");

            return _successor;
        }
    }
}
