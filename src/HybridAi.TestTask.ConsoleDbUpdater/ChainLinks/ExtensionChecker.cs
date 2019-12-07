using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class ExtensionChecker : ChainLink
    {
        public ExtensionChecker( IChainLink< Request, Response > successor ) 
            : base( successor )
        { }
    }
}
