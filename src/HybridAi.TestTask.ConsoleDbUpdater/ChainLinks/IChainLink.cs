using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public interface IChainLink< in TIn, out TOut >
        where TIn : Request 
        where TOut : Response
    {
        IChainLink< TIn, TOut >? Successor { get; }

        public TOut Process( TIn request )
        {
            var successor = Successor;

            if (successor != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return Successor.Process( request );
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return (TOut)new Response();
        }
    }
}
