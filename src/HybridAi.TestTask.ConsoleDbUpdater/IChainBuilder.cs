using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public interface IChainBuilder<out TOut> : IEnumerable< Type >
        where TOut : class, IChainLink< Request, IResponse< Request > >?
    {
        TOut? Result { get; }

        TOut Build();

        IChainBuilder< TOut > Append( IEnumerable< Type > chainTypes );
    }
}
