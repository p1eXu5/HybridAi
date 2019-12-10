using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Comparers;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Updater : ChainLink
    {
        public Updater( ChainLink? successor ) 
            : base( successor )
        { }

        public override IResponse<Request> Process( Request request )
        {

            if ( request is ImportedModelsRequest modelsRequest ) 
            {

                var colls = modelsRequest.ModelCollections;
                
                if ( colls!.Any() 
                    || colls.All( c => !c.Any() ) ) {
                    return new DoneRequest( "Imported model collections are empty." ).Response;
                }

                List< Type > types = colls.Aggregate( 
                    new List< Type >(), (a, b) => { a.Add(b.First().GetType() );
                    return a;} );

                if ( types.Any( t => typeof( City ).IsAssignableFrom( t ) ) 
                     && types.Any( t => typeof( CityBlock ).IsAssignableFrom( t ) ) )
                {
                    if ( _tryUpdateWhole( colls, out int newCount, out int updCount ) ) {
                        return new DoneRequest( $"There are {newCount} new records and {updCount} updated records." ).Response;
                    }
                }
                else {
                    if ( _tryUpdatePartial( colls, out int newCount, out int updCount ) ) {
                        return new DoneRequest( $"There are {newCount} new records and {updCount} updated records." ).Response;
                    }
                }
            }

            return base.Process( request );
        }

        private bool _tryUpdateWhole( List< IEntity >[] colls, out int newCount, out int updCount )
        {
            List< CityBlock > blocks = _getTypedAggregateCollection< IEntity, CityBlock >( colls );
            blocks.Sort( new CityBlockComparer() );

            List< City > cities = _getTypedAggregateCollection< IEntity, City >( colls );
            cities.Sort( new CityComparer() );

            List< CityBlock > dbBblocks = _getDbCityBlocks( blocks.First().CityLocationGeonameId, blocks.Last().CityLocationGeonameId );

            for( int i = 0; i < blocks.Count; ++i ) {
                for( int j = 0; j < cities.Count; ++j ) {

                }
            }

            throw new NotImplementedException();
        }


        private List< T2 > _getTypedAggregateCollection< T1, T2 >( List< T1 >[] coll )
            where T1 : IEntity
            where T2 : IEntity
        {
            return coll.Where( c => typeof( T2 ).IsAssignableFrom( c.First().GetType() ) )
                              .Aggregate( 
                                  new List< T2 >(),
                                  (agr, item) => { 
                                      agr.AddRange( item.Cast< T2 >() ); 
                                      return agr; 
                                  } );
        }


        private bool _tryUpdatePartial( List< IEntity >[] colls, out int newCount, out int updCount )
        {

            throw new NotImplementedException();
        }
    }
}
