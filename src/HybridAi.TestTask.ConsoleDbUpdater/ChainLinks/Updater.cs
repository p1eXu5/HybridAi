using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Comparators;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.UpdaterService;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Updater : ChainLink, IDisposable
    {
        #region fields

        private IpDbContext? _dbContext;
        private readonly (int, int) _fail = (1, 1);

        #endregion


        #region ctor

        public Updater(ChainLink? successor)
            : base(successor)
        { }

        #endregion


        #region properties

        public IpDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    var options = DbContextOptionsFactory.Instance.DbContextOptions;
                    _dbContext = new IpDbContext(options);
                }

                return _dbContext;
            }
        }

        #endregion


        #region methods

        public override IResponse<Request> Process(Request request)
        {

            if (request is ImportedModelsRequest modelsRequest)
            {

                var colls = modelsRequest.ModelCollections;

                if ( !colls.Any()
                    || colls.All(c => !c.Any()))
                {
                    return new FailRequest("Imported model collections are empty.").Response;
                }

                List<Type> types = colls.Aggregate(
                    new List<Type>(), (a, b) =>
                    {
                        a.Add(b.First().GetType());
                        return a;
                    });

                if (types.Any(t => typeof(CityBlock).IsAssignableFrom(t)))
                {
                    if (_updateCityBlocks(colls, out int newCount, out int updCount)) {
                        return new DoneRequest($"There are {newCount} new records and {updCount} updated records.").Response;
                    }

                    return new FailRequest($"There are {newCount} new records and {updCount} updated records.").Response;
                }
                else
                {
                    if (_updateCityLocations(colls, out int newCount, out int updCount)) {
                        return new DoneRequest($"There are {newCount} new records and {updCount} updated records.").Response;
                    }
                }
            }

            return base.Process(request);
        }


        private bool _updateCityBlocks(List<IEntity>[] colls, out int newCount, out int updCount)
        {
            List< CityBlock > blocks = _getTypedAggregateCollection< IEntity, CityBlock >(colls);
            blocks.Sort(new CityBlockComparer());

            List< CityLocation > cityLocations = _getTypedAggregateCollection< IEntity, CityLocation >(colls);
            if (!cityLocations.Any())
            {
                (newCount, updCount) = _updateCityBlocks( blocks );
                return (newCount, updCount) != _fail;
            }
            cityLocations.Sort( new CityLocationComparer() );

            int blkInd = 0, clkInd = 0;
            var locales = new Dictionary< string, LocaleCode >( 8 );

            while (blkInd < blocks.Count && clkInd < cityLocations.Count)
            {
                int geoId = blocks[blkInd].CityLocationGeonameId;
                int j = 0;

                while ( blkInd < blocks.Count && blocks[blkInd].CityLocationGeonameId == geoId)
                {
                    j = clkInd;

                    while ( j < cityLocations.Count && cityLocations[j].GeonameId == geoId )
                    {
                        City? city = blocks[blkInd].CopyCityLocationCity( cityLocations[ j ] ).FirstOrDefault();

                        if ( city?.LocaleCode?.Name != null ) {

                            if ( locales.TryGetValue( city.LocaleCode.Name, out var val ) ) {
                                city.LocaleCode = val;
                            }
                            else {
                                locales[ city.LocaleCode.Name ] = city.LocaleCode;
                            }
                        }

                        ++j;
                    }

                    ++blkInd;
                }
                clkInd = j;
            }

            (newCount, updCount) = _updateCityBlocks( blocks );
            return (newCount, updCount) != _fail;
        }

        private List<T2> _getTypedAggregateCollection<T1, T2>(List<T1>[] coll)
            where T1 : IEntity
            where T2 : IEntity
        {
            return coll.Where(c => c.First() is T2)
                              .Aggregate(
                                  new List<T2>(),
                                  (agr, item) =>
                                  {
                                      agr.AddRange(item.Cast<T2>());
                                      return agr;
                                  });
        }

        private (int newCount, int updCount) _updateCityBlocks(List< CityBlock > blocks)
        {
            (int newCount, int updCount) = (0, 0);
            var context = DbContext;

            List<CityBlock> dbBblocks = _getDbCityBlocks(
                blocks.First().CityLocationGeonameId, 
                blocks.Last().CityLocationGeonameId );

            try {
                if ( dbBblocks.Any() ) {

                }
                else {
                    context.AddRange( blocks );
                    newCount = blocks.Count;
                }

                context.SaveChanges();
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Log( ex.Message );
                return (0, 0);
            }

            return (newCount, updCount);
        }

        private bool _updateCityLocations(List<IEntity>[] colls, out int newCount, out int updCount)
        {

            throw new NotImplementedException();
        }

        private List<CityBlock> _getDbCityBlocks(int minGeonameId, int maxGeonameId)
        {
            var context = DbContext;

            return context.GetCityBlocks(minGeonameId, maxGeonameId);
        }

        #endregion


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
