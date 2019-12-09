using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Updater : ChainLink
    {
        public Updater( ChainLink? successor ) 
            : base( successor )
        { }

        public override IResponse<Request> Process( Request request )
        {
            if ( request is ImportedModelsRequest modelsRequest ) {
                if ( !modelsRequest.ModelCollections.Any() 
                    || modelsRequest.ModelCollections.All( c => !c.Any() ) ) {
                    return new DoneRequest( "Imported model collections are empty." ).Response;
                }
            }

            return base.Process( request );
        }
    }
}
