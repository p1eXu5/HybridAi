using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Comparers;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.UpdaterService;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Updater : ChainLink, IDisposable
    {
        private IpDbContext? _dbContext;
        private readonly (int,int) _fail = (1, 1);

        public Updater( ChainLink? successor ) 
            : base( successor )
        { }

        public IpDbContext DbContext
        {
            get {
                if ( _dbContext == null ) {
                    var options = DbContextOptionsFactory.Instance.DbContextOptions;
                    _dbContext = new IpDbContext( options );
                }

                return _dbContext;
            }
        }


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

                if ( types.Any( t => typeof( CityBlock ).IsAssignableFrom( t ) ) )
                {
                    if ( _composeBlocks( colls, out int newCount, out int updCount ) ) {
                        return new DoneRequest( $"There are {newCount} new records and {updCount} updated records." ).Response;
                    }
                    else {
                        return new FailRequest( $"There are {newCount} new records and {updCount} updated records." ).Response;
                    }
                }
                else {
                    if ( _updateCity( colls, out int newCount, out int updCount ) ) {
                        return new DoneRequest( $"There are {newCount} new records and {updCount} updated records." ).Response;
                    }
                }
            }

            return base.Process( request );
        }

        private bool _composeBlocks( List< IEntity >[] colls, out int newCount, out int updCount )
        {
            List< CityBlock > blocks = _getTypedAggregateCollection< IEntity, CityBlock >( colls );
            blocks.Sort( new CityBlockComparer() );

            List< City > cities = _getTypedAggregateCollection< IEntity, City >( colls );

            if ( !cities.Any() ) {
                (newCount, updCount) = _updateBlocks( blocks );
                return (newCount, updCount) == _fail; 
            }

            cities.Sort( new CityComparer() );

            List< CityBlock > dbBblocks = _getDbCityBlocks( blocks.First().CityLocationGeonameId, blocks.Last().CityLocationGeonameId );

            int blkInd = 0, cityInd = 0;

            while( blkInd < blocks.Count && cityInd < cities.Count ) 
            {
                int geoId = blocks[ blkInd ].CityLocationGeonameId;

                while( blocks[ blkInd ].CityLocationGeonameId == geoId )
                {
                    --cityInd;
                    while( cities[ cityInd ].GeonameId == geoId )
                    {
                        ++cityInd;
                        blocks[ blkInd ].CopyCity( cities[ cityInd ] );
                    }
                    ++blkInd;
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

        private bool _updateBlocks( List< IEntity >[] colls )
        {

            throw new NotImplementedException();
        }

        private bool _updateCity( List< IEntity >[] colls, out int newCount, out int updCount )
        {

            throw new NotImplementedException();
        }

        private List< CityBlock > _getDbCityBlocks( int minGeonameId, int maxGeonameId )
        {
            var ctx = DbContext;

            return ctx.GetCityBlocks(minGeonameId, maxGeonameId);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose( bool disposing )
        {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                    _dbContext?.Dispose();
                    _dbContext = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Updater()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
