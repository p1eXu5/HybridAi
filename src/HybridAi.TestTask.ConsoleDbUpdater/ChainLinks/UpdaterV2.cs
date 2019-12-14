using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Comparators;
using HybridAi.TestTask.Data.Services.UpdaterService;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class UpdaterV2 : Updater
    {
        public UpdaterV2( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }

        [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
        protected override IResponse< Request > _process( List< IEntity >[] importedEntities )
        {
            var( blocks, locations) = _decollateEntities( importedEntities );

            #region collections initializations

            List<LocaleCode>? localeCodes = null;
            List<City>? enCities = null;
            List<City>? esCities = null;
            List<City>? deCities = null;
            List<City>? frCities = null;
            List<City>? ruCities = null;
            List<City>? jaCities = null;
            List<City>? ptBrCities = null;
            List<City>? zhCnCities = null;
            List<CityLocation>? cityLocations = null;
            List<CityBlock>? cityBlockIpv4s = null;
            List<CityBlock>? cityBlockIpv6s = null;

            #endregion

            #region first task iteration

            Task? localeCodesTask = Task.Run(() => { localeCodes = _distinctLocaleCodes(locations).ToList(); });
            Task? enCitiesTask = Task.Run(() => { enCities = _distinctCities(locations, cl => cl.EnCity).ToList(); });
            Task? esCitiesTask = Task.Run(() => { esCities = _distinctCities(locations, cl => cl.EsCity).ToList(); });
            Task? deCitiesTask = Task.Run(() => { deCities = _distinctCities(locations, cl => cl.DeCity).ToList(); });
            Task? frCitiesTask = Task.Run(() => { frCities = _distinctCities(locations, cl => cl.FrCity).ToList(); });
            Task? ruCitiesTask = Task.Run(() => { ruCities = _distinctCities(locations, cl => cl.RuCity).ToList(); });
            Task? jaCitiesTask = Task.Run(() => { jaCities = _distinctCities(locations, cl => cl.JaCity).ToList(); });
            Task? ptBrCitiesTask = Task.Run(() => { ptBrCities = _distinctCities(locations, cl => cl.PtBrCity).ToList(); });
            Task? zhCnCitiesTask = Task.Run(() => { zhCnCities = _distinctCities(locations, cl => cl.ZhCnCity).ToList(); });
            Task? cityLocationsTask = Task.Run(() => { cityLocations = _distinctCityLocations(locations).ToList(); });
            Task? blockTask = Task.Run( () => { (cityBlockIpv4s, cityBlockIpv6s) = _decollateBlocks( blocks ); } );

            Task.WhenAll(new[] {
                localeCodesTask,
                enCitiesTask,
                esCitiesTask,
                deCitiesTask,
                frCitiesTask,
                ruCitiesTask,
                jaCitiesTask,
                ptBrCitiesTask,
                zhCnCitiesTask,
                cityLocationsTask,
                blockTask
            });

            #endregion


            localeCodesTask = null;
            enCitiesTask = null;
            esCitiesTask = null;
            deCitiesTask = null;
            frCitiesTask = null;
            ruCitiesTask = null;
            jaCitiesTask = null;
            ptBrCitiesTask = null;
            zhCnCitiesTask = null;
            cityLocationsTask = null;
            blockTask = null;
            Task? blockIpv6Task = null;

            var ctx = DbContext;

            if ( localeCodes != null ) {
                var dbLocaleCodes = ctx.LocaleCodes.ToArray();
                if ( dbLocaleCodes.Any() )
                    localeCodesTask = Task.Run( () => _updateLocaleCodes( dbLocaleCodes, localeCodes ) );
            }

            ruCitiesTask = enCities?.Any() == true ? _getCityTask( enCities, ( min, max ) => ctx.GetCities<EnCity>( min, max ) ) : null;
            deCitiesTask = esCities?.Any() == true ? _getCityTask( esCities, ( min, max ) => ctx.GetCities<EsCity>( min, max ) ) : null;
            ruCitiesTask = deCities?.Any() == true ? _getCityTask( deCities, ( min, max ) => ctx.GetCities<DeCity>( min, max ) ) : null;
            ruCitiesTask = frCities?.Any() == true ? _getCityTask( frCities, ( min, max ) => ctx.GetCities<FrCity>( min, max ) ) : null;
            ruCitiesTask = ruCities?.Any() == true ? _getCityTask( ruCities, ( min, max ) => ctx.GetCities<RuCity>( min, max ) ) : null;
            ruCitiesTask = jaCities?.Any() == true ? _getCityTask( jaCities, ( min, max ) => ctx.GetCities<JaCity>( min, max ) ) : null;
            ruCitiesTask = ptBrCities?.Any() == true ? _getCityTask( ptBrCities, ( min, max ) => ctx.GetCities<PtBrCity>( min, max ) ) : null;
            ruCitiesTask = zhCnCities?.Any() == true ? _getCityTask( zhCnCities, ( min, max ) => ctx.GetCities<ZhCnCity>( min, max ) ) : null;

            if ( cityBlockIpv4s?.Any() == true ) {
                var dbBlockIpv4s = ctx.GetCityBlockIpv4s( cityBlockIpv4s.Min( c => c.GetNetwork() ), cityBlockIpv4s.Max( c => c.GetNetwork() ) ).Cast<CityBlock>().ToArray();
                if ( dbBlockIpv4s.Any() )
                    blockTask = Task.Run( () => _updateCityBlocks( dbBlockIpv4s, cityBlockIpv4s ) );
            }

            if ( cityBlockIpv6s?.Any() == true ) {
                var dbBlockIpv6s = ctx.GetCityBlockIpv6s( cityBlockIpv6s.Min( c => c.GetNetwork() ), cityBlockIpv6s.Max( c => c.GetNetwork() ) ).Cast<CityBlock>().ToArray();
                if ( dbBlockIpv6s.Any() )
                    blockIpv6Task = Task.Run( () => _updateCityBlocks( dbBlockIpv6s, cityBlockIpv6s ) );
            }

            #nullable disable
            Task[] tasks = new [] {
                localeCodesTask,
                enCitiesTask,
            }.Where( t => t != null ).ToArray();
            #nullable restore

            if ( tasks.Any() == true ) {
                Task.WhenAll( tasks );
            }

            if ( localeCodes.Any() ) { ctx.AddRange( localeCodes ); }

            throw new NotImplementedException();
        }

        private Task? _getCityTask< T >( List< City > cities, Func<int, int, IQueryable<T>> getDbEntities )
        {
            var ctx = DbContext;
            var dbCities = getDbEntities( cities.Min( c => c.GeonameId ), cities.Max( c => c.GeonameId ) ).Cast< City >().ToArray();
            if ( dbCities.Any() )
                return Task.Run( () => _updateCities( dbCities, cities ) );

            return null;
        }

        private void _updateLocaleCodes( LocaleCode[] dbLocaleCodes, List< LocaleCode > localeCodes )
        {
            foreach ( var code in localeCodes.ToArray() ) {
                var dbCode = dbLocaleCodes.FirstOrDefault( c => code.Name.Equals( c.Name ) );
                if ( dbCode != null ) {
                    localeCodes.Remove( code );
                }
            }
        }

        private void _updateCities( City[] dbCities, List< City > cities )
        {
            foreach ( var city in cities.ToArray() ) {
                var dbCity = dbCities.FirstOrDefault( c => city.GeonameId.Equals( c.GeonameId ) );
                if ( dbCity != null ) {
                    cities.Remove( city );
                }
            }
        }

        private void _updateCityBlocks( CityBlock[] dbBlocks, List< CityBlock > blocks )
        {
            foreach ( var block in blocks.ToArray() ) {
                var dbBlock = dbBlocks.FirstOrDefault( b => block.GetNetwork().Equals( b.GetNetwork() ) );
                if ( dbBlock != null ) {
                    blocks.Remove( block );
                }
            }
        }

        protected (List< IEntity >, List< IEntity >) _decollateEntities( List<IEntity>[] entities )
        {
            List< IEntity > blocks = new List< IEntity >();
            List< IEntity > locations = new List< IEntity >();

            foreach ( var entity in entities ) {
                if ( entity[0] is CityBlock ) {
                    blocks.AddRange( entity );
                }
                else {
                     locations.AddRange( entity );
                }
            }

            return (blocks, locations);
        }

        protected (List< CityBlock >, List< CityBlock >) _decollateBlocks( List<IEntity> blocks )
        {
            throw new NotImplementedException();
        }

        protected IEnumerable< LocaleCode > _distinctLocaleCodes( List< IEntity > cityLocations )
        {
            return cityLocations.Cast< CityLocation >().SelectMany( e => {
                var list = new List< LocaleCode >(8);
                if (e.EnCity?.LocaleCode != null) list.Add( e.EnCity.LocaleCode );
                if (e.EsCity?.LocaleCode != null) list.Add( e.EsCity.LocaleCode );
                if (e.DeCity?.LocaleCode != null) list.Add( e.DeCity.LocaleCode );
                if (e.FrCity?.LocaleCode != null) list.Add( e.FrCity.LocaleCode );
                if (e.RuCity?.LocaleCode != null) list.Add( e.RuCity.LocaleCode );
                if (e.JaCity?.LocaleCode != null) list.Add( e.JaCity.LocaleCode );
                if (e.PtBrCity?.LocaleCode != null) list.Add( e.PtBrCity.LocaleCode );
                if (e.ZhCnCity?.LocaleCode != null) list.Add( e.ZhCnCity.LocaleCode );
                return list;
            } ).Distinct( new LocaleCodeComparer() );
        }


        protected IEnumerable< City > _distinctCities( List< IEntity > cityLocations, Func< CityLocation, City > getter )
        {
            return cityLocations.Cast< CityLocation >()
                                .Select( getter )
                                .Where( c => c != null )
                                .Distinct( new CityComparer() )
                                .Select( c => { if ( c.LocaleCode != null ) c.LocaleCode = null; return c; } );
        }

        protected IEnumerable< CityLocation > _distinctCityLocations( List< IEntity > cityLocations )
        {
            return cityLocations.Cast< CityLocation >()
                                .Distinct( new CityLocationComparer() )
                                .Select( cl => {
                                    if ( cl.EnCity != null ) cl.EnCity = null;
                                    if ( cl.EsCity != null ) cl.EsCity = null;
                                    if ( cl.DeCity != null ) cl.DeCity = null;
                                    if ( cl.FrCity != null ) cl.FrCity = null;
                                    if ( cl.RuCity != null ) cl.RuCity = null;
                                    if ( cl.EnCity != null ) cl.EnCity = null;
                                    if ( cl.EnCity != null ) cl.EnCity = null;
                                    if ( cl.EnCity != null ) cl.EnCity = null;
                                    return cl;
                                } );
        }
    }
}
