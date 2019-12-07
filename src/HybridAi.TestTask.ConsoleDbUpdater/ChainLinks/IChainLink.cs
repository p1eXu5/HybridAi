using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public interface IChainLink< in TIn, out TOut >
        where TIn : Request 
        where TOut : Request
    {
        IChainLink< TIn, TOut >? Successor { get; }

        TOut Process( TIn request );
    }
}
