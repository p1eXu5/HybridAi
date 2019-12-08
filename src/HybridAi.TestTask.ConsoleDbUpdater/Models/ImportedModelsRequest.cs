using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class ImportedModelsRequest : Request
    {
        public ImportedModelsRequest( List< IEntity >[] modelCollections )
        {
            ModelCollections = modelCollections;
        }

        public List< IEntity >[] ModelCollections { get; }

        public override IResponse< Request > Response => new Response< ImportedModelsRequest >( this );
    }
}
