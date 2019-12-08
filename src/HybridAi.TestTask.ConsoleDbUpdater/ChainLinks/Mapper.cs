using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Mapper : ChainLink
    {
        public Mapper( ChainLink? successor ) 
            : base( successor )
        { }

        public override IResponse< Request > Process( Request request )
        {
            if ( Successor != null && request is FolderRequest folderRequest ) 
            {
                var task = Task.Run( async () => 
                {
                    Task< ICollection< IEntity >? >[] tasks =
                        folderRequest.Files.Select( f => Task.Run( async () => await _map( f ) ) ).ToArray();
                    
                    return await Task.WhenAll( tasks );
                } );

                var result = task.Result.Where( hs => hs != null ).ToArray();

                if ( result.Any() == false ) {
                    return base.Process( request );
                }


                return base.Process( new ImportedModelsRequest( _consolidate( result ) ) );
            }

            return base.Process( request );
        }

        private async Task< ICollection< IEntity >? > _map( string fileName )
        {
            string? line = null;
            int n = 0;
            do {
                line = File.ReadLines( fileName ).Skip( n ).First();
                ++n;
            } while ( String.IsNullOrWhiteSpace( line ) );

            IModelMapper< HashSet< IEntity > >? mm = ModelMapperFactory.Instance.TryFindModelMapper( line );

            if ( mm != null ) {
                await mm.BuildModelCollectionAsync( fileName, n );
            }

            return mm?.Result;
        }

        private ICollection< IEntity >[] _consolidate( ICollection< IEntity >[] collections )
        {
            throw new NotImplementedException();
        }
    }
}
