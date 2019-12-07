﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public abstract class ChainLink : IChainLink< Request, IResponse< Request > >
    {
        private IChainLink< Request, IResponse< Request > >? _successor;

        protected ChainLink( IChainLink< Request, IResponse< Request > >? successor )
        {
            _successor = successor;
        }

        public IChainLink< Request, IResponse< Request > >? Successor => _successor;

        public IChainLink< Request, IResponse< Request > > SetSuccessor( IChainLink< Request, IResponse< Request > >? successor )
        {
            if (successor == null) return this;
            _successor = successor;

            return _successor;
        }

        public virtual IResponse< Request > Process( Request request )
        {
            var successor = Successor;

            if (successor != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return Successor.Process( request );
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return request.Response;
        }
    }
}
