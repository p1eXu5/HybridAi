using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class TempFileCreator : ChainLink
    {
        public TempFileCreator( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }

        public override IResponse< Request > Process( Request request )
        {
            throw new NotImplementedException( "TempFileCreator not implemented." );
            return base.Process( request );
        }
    }
}
