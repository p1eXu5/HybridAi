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
        public Mapper( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }


        public override IResponse< Request > Process( Request request )
        {
            LoggerFactory.Instance.Log( "Start parsing files..." );

            if ( request is FolderRequest folderRequest ) 
            {
                var task = Task.Run( async () => 
                {
                    Task< List< IEntity >? >[] tasks =
                        folderRequest.Files.Select( f => Task.Run( async () => await _map( f ) ) ).ToArray();

                    return await Task.WhenAll< List< IEntity >? >( tasks );
                } );

                var result = task.Result.Where( hs => hs?.Any() == true ).ToArray();

                if ( result.Any() == false ) {
                    return base.Process( request );
                }


                return base.Process( new ImportedModelsRequest( result ) );
            }

            return base.Process( request );
        }

        private async Task< List< IEntity >? > _map( string fileName )
        {
            string? line = null;
            int n = 0;
            do {
                try {
                    line = File.ReadLines( fileName ).Skip( n ).First();
                }
                catch( Exception ex ) {
                    LoggerFactory.Instance.Log( ex.Message );
                    return null;
                }
                ++n;
            } while ( String.IsNullOrWhiteSpace( line ) );

            IModelMapper< List< IEntity > >? mm = ModelMapperFactory.Instance.TryFindModelMapper( line );

            if ( mm != null ) {
                await mm.BuildModelCollectionAsync( fileName, n );
            }

            return mm?.Result;
        }

    }
}
