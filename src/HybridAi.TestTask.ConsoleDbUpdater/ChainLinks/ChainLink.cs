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
        private IChainLink< Request, Response >? _successor;

        protected ChainLink( IChainLink< Request, Response >? successor )
        {
            _successor = successor;
        }

        public IChainLink< Request, Response >? Successor => _successor;

        public IChainLink< Request, Response > SetSuccessor( IChainLink< Request, Response >? successor )
        {
            if (successor == null) return this;
            _successor = successor;

            return _successor;
        }
    }
}
