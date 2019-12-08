using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Unzipper : ChainLink

    {
        public Unzipper( ChainLink? successor ) 
            : base( successor )
        { }

        public override IResponse< Request > Process( Request request )
        {
            return base.Process( request );
        }
    }
}
