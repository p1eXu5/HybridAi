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
                        return new DoneRequest( newCount, updCount, $"There are {newCount} new records and {updCount} updated records." ).Response;
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

        /// <summary>
        /// Merges city blocks with city locations, save it in database and sets new and update records count. 
        /// </summary>
        /// <param name="colls"></param>
        /// <param name="newCount"></param>
        /// <param name="updCount"></param>
        /// <returns></returns>
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

            _mergeCityBlocks( blocks, cityLocations );

            (newCount, updCount) = _updateCityBlocks( blocks );
            return (newCount, updCount) != _fail;
        }

        /// <summary>
        /// Merge city blocks with city locations and unifies locale codes.
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="cityLocations"></param>
        private void _mergeCityBlocks( List< CityBlock > blocks, List< CityLocation > cityLocations )
        {
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
        }

        private (int newCount, int updCount) _updateCityBlocks(List< CityBlock > blocks)
        {
            (int newCount, int updCount) = (0, 0);
            var context = DbContext;
            List<CityBlock> dbBblocks = context.GetCityBlocks(
                blocks.First().CityLocationGeonameId, 
                blocks.Last().CityLocationGeonameId );

            try {
                if ( dbBblocks.Any() ) {
                    updCount = _updateCityBlocks( dbBblocks, ref blocks );
                }

                if ( blocks.Any() ) {
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


        private int _updateCityBlocks( List< CityBlock > dbBlocks, ref List< CityBlock > impBlocks )
        {
            int updCount = 0;
            var arr = new CityBlock[ dbBlocks.Count ];
            var innerImpBlocks = impBlocks;


            Parallel.For( 0, dbBlocks.Count, ( i, s ) => {

                var impBlock = innerImpBlocks.FirstOrDefault( b => b.GetNetwork().Equals( dbBlocks[i].GetNetwork() ) );
                if ( impBlock == null ) return;

                if ( _updateDbCityBlock( dbBlocks[i], impBlock ) ) {
                    arr[i] = impBlock;
                }

                innerImpBlocks.Remove( impBlock );

            } );

            var set = new HashSet< CityBlock >( arr );
            set.SymmetricExceptWith( impBlocks );
            impBlocks = set.ToList();

            return updCount;
        }


        private bool _updateDbCityBlock( CityBlock dbBlock, CityBlock impBlock )
        {
            CityBlockExtensions.CopyProperty< CityBlock, int? >( dbBlock, impBlock, nameof( CityBlock.RegistredCountryGeonameId ) );
            CityBlockExtensions.CopyProperty< CityBlock, int? >( dbBlock, impBlock, nameof( CityBlock.RepresentedCountryGeonameId ) );
            CityBlockExtensions.CopyProperty< CityBlock, bool >( dbBlock, impBlock, nameof( CityBlock.IsAnonymousProxy ) );
            CityBlockExtensions.CopyProperty< CityBlock, bool >( dbBlock, impBlock, nameof( CityBlock.IsSatelliteProvider ) );
            CityBlockExtensions.CopyProperty< CityBlock, string >( dbBlock, impBlock, nameof( CityBlock.PostalCode ) );
            CityBlockExtensions.CopyProperty< CityBlock, double >( dbBlock, impBlock, nameof( CityBlock.Latitude ) );
            CityBlockExtensions.CopyProperty< CityBlock, double >( dbBlock, impBlock, nameof( CityBlock.Longitude ) );
            CityBlockExtensions.CopyProperty< CityBlock, int >( dbBlock, impBlock, nameof( CityBlock.CityLocationGeonameId ) );

            var dbCityLocation = dbBlock.CityLocation;
            _updateDbCityLocation( ref dbCityLocation, impBlock.CityLocation );

            // feature will be added on request 
            return true;
        }

        private bool _updateDbCityLocation( ref CityLocation dbCityLocation, CityLocation impCityLocation )
        {
            if ( impCityLocation == null ) return false;
            
            if ( dbCityLocation == null ) 
            {
                var ctx = DbContext;
                var db = ctx.CityLocations.FirstOrDefault( cl => cl.GeonameId == impCityLocation.GeonameId );

                if ( db != null ) {
                    dbCityLocation =  db;
                    return true;
                }

                dbCityLocation = new CityLocation( impCityLocation.GeonameId ) {
                    ContinentCode = impCityLocation.ContinentCode,
                    CountryIsoCode = impCityLocation.CountryIsoCode,
                    Subdivision1IsoCode = impCityLocation.Subdivision1IsoCode,
                    Subdivision2IsoCode = impCityLocation.Subdivision2IsoCode,
                    MetroCode = impCityLocation.MetroCode,
                    TimeZone = impCityLocation.TimeZone,
                    IsInEuropeanUnion = impCityLocation.IsInEuropeanUnion
                };
            }
            else {
                CityBlockExtensions.CopyProperty< CityLocation, string >( dbCityLocation, impCityLocation, nameof( CityLocation.Subdivision1IsoCode ) );
                CityBlockExtensions.CopyProperty< CityLocation, string? >( dbCityLocation, impCityLocation, nameof( CityLocation.Subdivision2IsoCode ) );
                CityBlockExtensions.CopyProperty< CityLocation, string? >( dbCityLocation, impCityLocation, nameof( CityLocation.MetroCode ) );
                CityBlockExtensions.CopyProperty< CityLocation, bool >( dbCityLocation, impCityLocation, nameof( CityLocation.IsInEuropeanUnion ) );
            }

            City dbEnCity = dbCityLocation.EnCity;
            _updateDbCity( ref dbEnCity, impCityLocation.EnCity, c => new EnCity( c.GeonameId ) );

            City dbRuCity = dbCityLocation.RuCity;
            _updateDbCity( ref dbRuCity, impCityLocation.RuCity, c => new RuCity( c.GeonameId ) );

            City dbFrCity = dbCityLocation.FrCity;
            _updateDbCity( ref dbFrCity, impCityLocation.FrCity, c => new FrCity( c.GeonameId ) );

            City dbDeCity = dbCityLocation.DeCity;
            _updateDbCity( ref dbDeCity, impCityLocation.DeCity, c => new DeCity( c.GeonameId ) );

            City dbEsCity = dbCityLocation.EsCity;
            _updateDbCity( ref dbEsCity, impCityLocation.EsCity, c => new EsCity( c.GeonameId ) );

            City dbJaCity = dbCityLocation.JaCity;
            _updateDbCity( ref dbJaCity, impCityLocation.JaCity, c => new JaCity( c.GeonameId ) );

            City dbPtBrCity = dbCityLocation.PtBrCity;
            _updateDbCity( ref dbPtBrCity, impCityLocation.PtBrCity, c => new PtBrCity( c.GeonameId ) );

            City dbZhCnCity = dbCityLocation.ZhCnCity;
            _updateDbCity( ref dbZhCnCity, impCityLocation.ZhCnCity, c => new ZhCnCity( c.GeonameId ) );
            
            // feature will be added on request 
            return true;
        }


        private bool _updateDbCity( ref City dbCity, City impCity, Func< City, City > factory )
        {
            if ( impCity == null ) return false;

            if ( dbCity == null ) 
            {
                var ctx = DbContext;
                var db = ctx.GetCity( impCity );
                if ( db != null ) {
                    dbCity = db;
                    return true;
                }

                dbCity = factory( impCity );
                dbCity.ContinentName = impCity.ContinentName;
                dbCity.CountryName = impCity.CountryName;
                dbCity.Subdivision1Name = impCity.Subdivision1Name;
                dbCity.Subdivision2Name = impCity.Subdivision2Name;
                dbCity.CityName = impCity.CityName;

            }
            else {
                CityBlockExtensions.CopyProperty< City, string >( dbCity, impCity, nameof( City.ContinentName) );
                CityBlockExtensions.CopyProperty< City, string >( dbCity, impCity, nameof( City.CountryName) );
                CityBlockExtensions.CopyProperty< City, string >( dbCity, impCity, nameof( City.Subdivision1Name) );
                CityBlockExtensions.CopyProperty< City, string >( dbCity, impCity, nameof( City.Subdivision2Name) );
                CityBlockExtensions.CopyProperty< City, string >( dbCity, impCity, nameof( City.CityName) );
            }

            LocaleCode locale = dbCity.LocaleCode;
            _updateDbLocale( ref locale, impCity.LocaleCode );

            // feature will be added on request 
            return true;
        }

        private bool _updateDbLocale( ref LocaleCode dbLocale, LocaleCode impLocale )
        {
            if ( impLocale == null ) return false;

            if ( dbLocale == null ) 
            {
                var ctx = DbContext;
                var db = ctx.LocaleCodes.FirstOrDefault( l => l.Name.Equals( impLocale.Name ) );
                if ( db != null ) {
                    dbLocale = db;
                    return true;
                }

                dbLocale = impLocale;
            }

            // feature will be added on request 
            return true;
        }

        private bool _updateCityLocations(List<IEntity>[] colls, out int newCount, out int updCount)
        {
            List< CityLocation > cityLocations = _getTypedAggregateCollection< IEntity, CityLocation >(colls);
            cityLocations.Sort( new CityLocationComparer() );

           _mergeCityLocations( ref cityLocations );

            (newCount, updCount) = _updateCityLocations( cityLocations );
            return (newCount, updCount) != _fail;
        }

        private void _mergeCityLocations( ref List< CityLocation > cityLocations )
        {
            var locales = new Dictionary< string, LocaleCode >( 8 );
            CityBlock fakeBlock = new CityBlock();
            var transformedCityLocations = new List< CityLocation >( cityLocations.Count + 1 ) { 
                new CityLocation( cityLocations[0].GeonameId )
            };

            int i = 0;
            for ( ; i < cityLocations.Count; ++i ) 
            {
                if ( transformedCityLocations.Last().GeonameId != cityLocations[i].GeonameId ) {
                    transformedCityLocations.Add( fakeBlock.CityLocation );
                    fakeBlock = new CityBlock();
                }

                City? city = fakeBlock.CopyCityLocationCity( cityLocations[i] ).FirstOrDefault();

                if ( city?.LocaleCode?.Name != null ) {

                    if ( locales.TryGetValue( city.LocaleCode.Name, out var val ) ) {
                        city.LocaleCode = val;
                    }
                    else {
                        locales[ city.LocaleCode.Name ] = city.LocaleCode;
                    }
                }
            }

            --i;

            if ( transformedCityLocations.Last().GeonameId != cityLocations[i].GeonameId ) {
                    transformedCityLocations.Add( fakeBlock.CityLocation );
            }

            transformedCityLocations.RemoveAt(0);
            cityLocations = transformedCityLocations;
        }


        private (int newCount, int updCount) _updateCityLocations(List< CityLocation > cityLocations )
        {
            (int newCount, int updCount) = (0, 0);
            var context = DbContext;

            List< CityLocation > dbBblocks = context.GetCityLocations(
                cityLocations.First().GeonameId, 
                cityLocations.Last().GeonameId ).ToList();

            try {
                if ( dbBblocks.Any() ) {

                }
                else {
                    context.AddRange( cityLocations );
                    newCount = cityLocations.Count;
                }

                context.SaveChanges();
            }
            catch (Exception ex) {
                LoggerFactory.Instance.Log( ex.Message );
                return (0, 0);
            }

            return (newCount, updCount);
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
