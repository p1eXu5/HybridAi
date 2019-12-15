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
        protected override IResponse< Request > _Process( List< IEntity >[] importedEntities )
        {
            var( blocks, locations) = _decollateEntities( importedEntities );

            #region collections initializations

            List<LocaleCode> localeCodes = new List< LocaleCode >(0);
            List<City> enCities = new List< City >(0);
            List<City> esCities = new List< City >(0);
            List<City> deCities = new List< City >(0);
            List<City> frCities = new List< City >(0);
            List<City> ruCities = new List< City >(0);
            List<City> jaCities = new List< City >(0);
            List<City> ptBrCities = new List< City >(0);
            List<City> zhCnCities = new List< City >(0);
            List<CityLocation> cityLocations = new List< CityLocation >(0);
            List<CityBlock> cityBlockIpv4s = new List< CityBlock >(0);
            List<CityBlock> cityBlockIpv6s = new List< CityBlock >(0);

            #endregion

            #region first task iteration

            LoggerFactory.Instance.Log( "\tPreparing locales..." );

            localeCodes = _distinctLocaleCodes(locations).ToList();

            Task? enCitiesTask = Task.Run(() => { enCities = _distinctCities(locations, cl => cl.EnCity).ToList(); });
            Task? esCitiesTask = Task.Run(() => { esCities = _distinctCities(locations, cl => cl.EsCity).ToList(); });
            Task? deCitiesTask = Task.Run(() => { deCities = _distinctCities(locations, cl => cl.DeCity).ToList(); });
            Task? frCitiesTask = Task.Run(() => { frCities = _distinctCities(locations, cl => cl.FrCity).ToList(); });
            Task? ruCitiesTask = Task.Run(() => { ruCities = _distinctCities(locations, cl => cl.RuCity).ToList(); });
            Task? jaCitiesTask = Task.Run(() => { jaCities = _distinctCities(locations, cl => cl.JaCity).ToList(); });
            Task? ptBrCitiesTask = Task.Run(() => { ptBrCities = _distinctCities(locations, cl => cl.PtBrCity).ToList(); });
            Task? zhCnCitiesTask = Task.Run(() => { zhCnCities = _distinctCities(locations, cl => cl.ZhCnCity).ToList(); });
            Task? blockTask = Task.Run( () => { (cityBlockIpv4s, cityBlockIpv6s) = _decollateBlocks( blocks ); } );

            LoggerFactory.Instance.Log( "\tPreparing cities..." );

            Task.WhenAll(new[] {
                enCitiesTask,
                esCitiesTask,
                deCitiesTask,
                frCitiesTask,
                ruCitiesTask,
                jaCitiesTask,
                ptBrCitiesTask,
                zhCnCitiesTask,
                blockTask
            }).Wait();


            LoggerFactory.Instance.Log( "\tPreparing city locations..." );
            cityLocations = _distinctCityLocations(locations).ToList();

            #endregion


            var ctx = DbContext;

#nullable disable

            #region second iteration

            (int upd, int ext)[] updExtCounts = new (int, int)[12];


            const int LOCALES = 0;
            const int EN = 1;
            const int ES = 2;
            const int DE = 3;
            const int FR = 4;
            const int RU = 5;
            const int JA = 6;
            const int PT = 7;
            const int ZH = 8;
            const int LOCATIONS = 9;
            const int IP4 = 10;
            const int IP6 = 11;


            if ( localeCodes.Any() ) {
                var dbLocaleCodes = ctx.LocaleCodes.ToArray();
                if (dbLocaleCodes.Any()) {
                    updExtCounts[LOCALES] = _updateLocaleCodes(dbLocaleCodes, localeCodes);
                }
            }

            enCitiesTask = enCities.Any() ? _getUpdateCityTask(enCities, (min, max) => ctx.GetCities<EnCity>(min, max), updExtCounts, EN ) : null;
            esCitiesTask = esCities.Any() ? _getUpdateCityTask(esCities, (min, max) => ctx.GetCities<EsCity>(min, max), updExtCounts, ES) : null;
            deCitiesTask = deCities.Any() ? _getUpdateCityTask(deCities, (min, max) => ctx.GetCities<DeCity>(min, max), updExtCounts, DE) : null;
            frCitiesTask = frCities.Any() ? _getUpdateCityTask(frCities, (min, max) => ctx.GetCities<FrCity>(min, max), updExtCounts, FR) : null;
            ruCitiesTask = ruCities.Any() ? _getUpdateCityTask(ruCities, (min, max) => ctx.GetCities<RuCity>(min, max), updExtCounts, RU) : null;
            jaCitiesTask = jaCities.Any() ? _getUpdateCityTask(jaCities, (min, max) => ctx.GetCities<JaCity>(min, max), updExtCounts, JA) : null;
            ptBrCitiesTask = ptBrCities.Any() ? _getUpdateCityTask(ptBrCities, (min, max) => ctx.GetCities<PtBrCity>(min, max), updExtCounts, PT) : null;
            zhCnCitiesTask = zhCnCities.Any() ? _getUpdateCityTask(zhCnCities, (min, max) => ctx.GetCities<ZhCnCity>(min, max), updExtCounts, ZH) : null;

            Task[] tasks1 = new[] {
                enCitiesTask,
                esCitiesTask,
                deCitiesTask,
                frCitiesTask,
                ruCitiesTask,
                jaCitiesTask,
                ptBrCitiesTask,
                zhCnCitiesTask,
                //cityLocationsTask,
                //blockTask,
                //blockIpv6Task
            }.Where(t => t != null).ToArray();

            if (tasks1.Any() == true)
            {
                LoggerFactory.Instance.Log( "\tCheck updated cities..." );
                Task.WhenAll(tasks1).Wait();
            }

            Task? cityLocationsTask = null;
            if (cityLocations.Any())
            {
                var dbCityLocations = ctx.GetCityLocations(cityLocations.Min(c => c.GeonameId), cityLocations.Max(c => c.GeonameId)).ToArray();
                if (dbCityLocations.Any())
                {
                    cityLocationsTask = Task.Run(() => updExtCounts[LOCATIONS] = _updateCityLocations(dbCityLocations, cityLocations));
                }
            }

            blockTask = null;
            if (cityBlockIpv4s.Any())
            {
                var dbBlockIpv4s = ctx.GetCityBlockIpv4s(cityBlockIpv4s.Min(c => c.GetNetwork()), cityBlockIpv4s.Max(c => c.GetNetwork())).Cast<CityBlock>().ToArray();
                if (dbBlockIpv4s.Any())
                {
                    blockTask = Task.Run(() => updExtCounts[IP4] = _updateCityBlocks(dbBlockIpv4s, cityBlockIpv4s));
                }
            }

            Task? blockIpv6Task = null;
            if (cityBlockIpv6s.Any())
            {
                var dbBlockIpv6s = ctx.GetCityBlockIpv6s(cityBlockIpv6s.Min(c => c.GetNetwork()), cityBlockIpv6s.Max(c => c.GetNetwork())).Cast<CityBlock>().ToArray();
                if (dbBlockIpv6s.Any())
                {
                    blockIpv6Task = Task.Run(() => updExtCounts[IP6] = _updateCityBlocks(dbBlockIpv6s, cityBlockIpv6s));
                }
            }


            Task[] tasks = new[] {
                cityLocationsTask,
                blockTask,
                blockIpv6Task
            }.Where(t => t != null).ToArray();

            if (tasks.Any() == true)
            {
                LoggerFactory.Instance.Log( "\tCheck updated data..." );
                Task.WhenAll(tasks).Wait();
            }

            #endregion



            int newCount = 0;

            LoggerFactory.Instance.Log( "\tAdd locales..." );
            newCount += localeCodes.Count - updExtCounts[LOCALES].ext;
            newCount += enCities.Count - updExtCounts[EN].ext;
            newCount += esCities.Count - updExtCounts[ES].ext;
            newCount += deCities.Count - updExtCounts[DE].ext;
            newCount += frCities.Count - updExtCounts[FR].ext;
            newCount += ruCities.Count - updExtCounts[RU].ext;
            newCount += jaCities.Count - updExtCounts[JA].ext;
            newCount += ptBrCities.Count - updExtCounts[PT].ext;
            newCount += zhCnCities.Count - updExtCounts[ZH].ext;
            newCount += cityLocations.Count - updExtCounts[LOCATIONS].ext;
            newCount += cityBlockIpv4s.Count - updExtCounts[IP4].ext;
            newCount += cityBlockIpv6s.Count - updExtCounts[IP6].ext;

            try {
                #region add entities to db context
                
                ctx.AddRange(localeCodes);

                LoggerFactory.Instance.Log( "\tAdd en cities..." );
                ctx.AddRange(enCities);

                LoggerFactory.Instance.Log( "\tAdd es cities..." );
                ctx.AddRange(esCities);

                LoggerFactory.Instance.Log( "\tAdd de cities..." );
                ctx.AddRange(deCities);

                LoggerFactory.Instance.Log( "\tAdd fr cities..." );
                ctx.AddRange(frCities);

                LoggerFactory.Instance.Log( "\tAdd ru cities..." );
                ctx.AddRange(ruCities);

                LoggerFactory.Instance.Log( "\tAdd ja cities..." );
                ctx.AddRange(jaCities);

                LoggerFactory.Instance.Log( "\tAdd pt-Br cities..." );
                ctx.AddRange(ptBrCities);

                LoggerFactory.Instance.Log( "\tAdd zh-Cn cities..." );
                ctx.AddRange(zhCnCities);

                LoggerFactory.Instance.Log( "\tAdd locations..." );
                ctx.AddRange(cityLocations);

                LoggerFactory.Instance.Log( "\tAdd ip v.4..." );
                ctx.AddRange(cityBlockIpv4s);

                LoggerFactory.Instance.Log( "\tAdd ip v.6..." );
                ctx.AddRange(cityBlockIpv6s);

                #endregion

#nullable restore

                ctx.SaveChanges();
            }
            catch ( Exception ex ) {
                LoggerFactory.Instance.Log( ex.Message );

                if ( !String.IsNullOrEmpty( ex.InnerException?.Message ) ) {
                    LoggerFactory.Instance.Log( ex.Message );
                }
                return _GetFailRequest();
            }

            return _GetDoneRequest( newCount, updExtCounts.Sum( t => t.upd) );
        }


        #region update methods

        private Task? _getUpdateCityTask<T>(List<City> cities, Func<int, int, IQueryable<T>> getDbEntities, (int upd, int ext)[] updCounts, int ind )
        {
            var ctx = DbContext;
            var dbCities = getDbEntities(cities.Min(c => c.GeonameId), cities.Max(c => c.GeonameId)).Cast<City>().ToArray();
            if (dbCities.Any())
                return Task.Run(() => updCounts[ ind ] = _updateCities(dbCities, cities));

            return null;
        }

        private (int, int) _updateLocaleCodes(LocaleCode[] dbLocaleCodes, List<LocaleCode> localeCodes)
        {
            var existed = 0;

            foreach (var code in localeCodes.ToArray())
            {
                var dbCode = dbLocaleCodes.FirstOrDefault(c => code.Name.Equals(c.Name));
                if (dbCode != null)
                {
                    localeCodes.Remove(code);
                }
            }

            return (0, existed);
        }

        private (int, int) _updateCities(City[] dbCities, List<City> cities)
        {
            int updCount = 0, existed = 0;

            var runningCities = cities.ToArray();

            Parallel.For( 0, runningCities.Length, (i) => {

                var city = runningCities[i];

                var dbCity = dbCities.FirstOrDefault(c => city.GeonameId.Equals(c.GeonameId));
                if (dbCity != null)
                {
                    var isChanged = false;
                    
                    if ( !String.IsNullOrWhiteSpace( city.CityName ) && dbCity.CityName?.Equals( city.CityName ) == false ) {
                        dbCity.CityName = city.CityName;
                        isChanged = true;
                    }

                    if ( !String.IsNullOrWhiteSpace( city.ContinentName ) && dbCity.ContinentName?.Equals( city.ContinentName ) == false ) {
                        dbCity.ContinentName = city.ContinentName;
                        isChanged = true;
                    }

                    if ( !String.IsNullOrWhiteSpace( city.CountryName ) && dbCity.CountryName?.Equals( city.CountryName ) == false ) {
                        dbCity.CountryName = city.CountryName;
                        isChanged = true;
                    }

                    if ( !String.IsNullOrWhiteSpace( city.Subdivision1Name ) && dbCity.Subdivision1Name?.Equals( city.Subdivision1Name ) == false ) {
                        dbCity.Subdivision1Name = city.Subdivision1Name;
                        isChanged = true;
                    }

                    if ( !String.IsNullOrWhiteSpace( city.Subdivision2Name ) && dbCity.Subdivision2Name?.Equals( city.Subdivision2Name ) == false ) {
                        dbCity.Subdivision1Name = city.Subdivision1Name;
                        isChanged = true;
                    }

                    cities.Remove(city);
                    ++existed;

                    if ( isChanged ) { ++updCount; }
                }

            } );

            return (updCount, existed);
        }

        private (int, int) _updateCityLocations(CityLocation[] dbCityLocations, List<CityLocation> cityLocations)
        {
            int updCount = 0, existed = 0;

            var runningCityLocations = cityLocations.ToArray();

            Parallel.For( 0, runningCityLocations.Length, (i) => {

                var clk = runningCityLocations[i];

                var dbClk = dbCityLocations.FirstOrDefault(c => clk.GeonameId.Equals(c.GeonameId));
                if (dbClk != null)
                {
                    var isChanged = false;

                    if ( !String.IsNullOrWhiteSpace( clk.Subdivision1IsoCode ) && dbClk.Subdivision1IsoCode?.Equals( clk.Subdivision1IsoCode ) == false ) {
                        dbClk.Subdivision1IsoCode = clk.Subdivision1IsoCode;
                        isChanged = true;
                    }

                    if ( !String.IsNullOrWhiteSpace( clk.Subdivision2IsoCode ) && dbClk.Subdivision2IsoCode?.Equals( clk.Subdivision2IsoCode ) == false ) {
                        dbClk.Subdivision2IsoCode = clk.Subdivision2IsoCode;
                        isChanged = true;
                    }

                    if ( !String.IsNullOrWhiteSpace( clk.MetroCode ) && dbClk.MetroCode?.Equals( clk.MetroCode ) == false ) {
                        dbClk.MetroCode = clk.MetroCode;
                        isChanged = true;
                    }

                    if ( dbClk.IsInEuropeanUnion != clk.IsInEuropeanUnion ) { dbClk.IsInEuropeanUnion = clk.IsInEuropeanUnion; isChanged = true; }

                    if ( dbClk.EnCityGeonameId == null && clk.EnCityGeonameId != null) { dbClk.EnCityGeonameId = clk.EnCityGeonameId; isChanged = true; }
                    if ( dbClk.EsCityGeonameId == null && clk.EsCityGeonameId != null) { dbClk.EsCityGeonameId = clk.EsCityGeonameId; isChanged = true; }
                    if ( dbClk.DeCityGeonameId == null && clk.DeCityGeonameId != null) { dbClk.DeCityGeonameId = clk.DeCityGeonameId; isChanged = true; }
                    if ( dbClk.FrCityGeonameId == null && clk.FrCityGeonameId != null) { dbClk.FrCityGeonameId = clk.FrCityGeonameId; isChanged = true; }
                    if ( dbClk.RuCityGeonameId == null && clk.RuCityGeonameId != null) { dbClk.RuCityGeonameId = clk.RuCityGeonameId; isChanged = true; }
                    if ( dbClk.JaCityGeonameId == null && clk.JaCityGeonameId != null) { dbClk.JaCityGeonameId = clk.JaCityGeonameId; isChanged = true; }
                    if ( dbClk.PtBrCityGeonameId == null && clk.PtBrCityGeonameId != null) { dbClk.PtBrCityGeonameId = clk.PtBrCityGeonameId; isChanged = true; }
                    if ( dbClk.ZhCnCityGeonameId == null && clk.ZhCnCityGeonameId != null) { dbClk.ZhCnCityGeonameId = clk.ZhCnCityGeonameId; isChanged = true; }

                    cityLocations.Remove(clk);
                    ++existed;

                    if ( isChanged ) { ++updCount; }
                }
            });



            return (updCount, existed);
        }

        private (int, int) _updateCityBlocks(CityBlock[] dbBlocks, List<CityBlock> blocks)
        {
            int updCount = 0, existed = 0;

            foreach (var block in blocks.ToArray())
            {
                var dbBlock = dbBlocks.FirstOrDefault(b => block.GetNetwork().Equals(b.GetNetwork()));
                if (dbBlock != null)
                {
                    var isChanged = false;
                    if ( dbBlock.CityLocationGeonameId == null && block.CityLocationGeonameId != null) { dbBlock.CityLocationGeonameId = block.CityLocationGeonameId; isChanged = true; }
                    if ( dbBlock.RegistredCountryGeonameId == null && block.RegistredCountryGeonameId != null) { dbBlock.RegistredCountryGeonameId = block.RegistredCountryGeonameId; isChanged = true; }
                    if ( dbBlock.RepresentedCountryGeonameId == null && block.RepresentedCountryGeonameId != null) { dbBlock.RepresentedCountryGeonameId = block.RepresentedCountryGeonameId; isChanged = true; }

                    if ( dbBlock.IsAnonymousProxy != block.IsAnonymousProxy ) { dbBlock.IsAnonymousProxy = block.IsAnonymousProxy; isChanged = true; }
                    if ( dbBlock.IsSatelliteProvider != block.IsSatelliteProvider ) { dbBlock.IsSatelliteProvider = block.IsSatelliteProvider; isChanged = true; }

                    if ( dbBlock.Latitude == null && block.Latitude != null) { dbBlock.Latitude = block.Latitude; isChanged = true; }
                    if ( dbBlock.Longitude == null && block.Longitude != null) { dbBlock.Longitude = block.Longitude; isChanged = true; }
                    if ( dbBlock.AccuracyRadius == null && block.AccuracyRadius != null) { dbBlock.AccuracyRadius = block.AccuracyRadius; isChanged = true; }

                    blocks.Remove(block);
                    ++existed;
                    if ( isChanged ) { ++updCount; }
                }
            }

            return (updCount, existed);
        }

        #endregion


        #region decollate and distinct methods

        /// <summary>
        /// Decollates CityBlocks and CityLocations without distinct.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected (List<IEntity> blocks, List<IEntity> locations) _decollateEntities(List<IEntity>[] entities)
        {
            List<IEntity> blocks = new List<IEntity>();
            List<IEntity> locations = new List<IEntity>();

            foreach (var entity in entities)
            {
                if (entity[0] is CityBlock)
                {
                    blocks.AddRange(entity);
                }
                else
                {
                    locations.AddRange(entity);
                }
            }

            return (blocks, locations);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected (List<CityBlock> ipv4s, List<CityBlock> ipv6s) _decollateBlocks(List<IEntity> blocks)
        {
            var ipv4s = new List<CityBlock>();
            var ipv6s = new List<CityBlock>();

            foreach (var block in blocks)
            {
                if (block is CityBlockIpv4 ipv4)
                {
                    ipv4s.Add(ipv4);
                }
                else if (block is CityBlockIpv6 ipv6)
                {
                    ipv6s.Add(ipv6);
                }
            }

            return (ipv4s, ipv6s);
        }

        protected IEnumerable<LocaleCode> _distinctLocaleCodes(List<IEntity> cityLocations)
        {
            return cityLocations.Cast<CityLocation>().SelectMany(e =>
            {
                var list = new List<LocaleCode>(8);
                if (e.EnCity?.LocaleCode != null) list.Add(e.EnCity.LocaleCode);
                if (e.EsCity?.LocaleCode != null) list.Add(e.EsCity.LocaleCode);
                if (e.DeCity?.LocaleCode != null) list.Add(e.DeCity.LocaleCode);
                if (e.FrCity?.LocaleCode != null) list.Add(e.FrCity.LocaleCode);
                if (e.RuCity?.LocaleCode != null) list.Add(e.RuCity.LocaleCode);
                if (e.JaCity?.LocaleCode != null) list.Add(e.JaCity.LocaleCode);
                if (e.PtBrCity?.LocaleCode != null) list.Add(e.PtBrCity.LocaleCode);
                if (e.ZhCnCity?.LocaleCode != null) list.Add(e.ZhCnCity.LocaleCode);
                return list;
            }).Distinct(new LocaleCodeComparer());
        }

        protected IEnumerable<City> _distinctCities(List<IEntity> cityLocations, Func<CityLocation, City> getter)
        {
            return cityLocations.Cast<CityLocation>()
                                .Select(getter)
                                .Where(c => c != null)
                                .Distinct(new CityComparer())
                                .Select(c => {
                                    if (c.LocaleCode != null) {
                                        c.LocaleCode = null; 
                                    }
                                    c.CityLocation = null;
                                    return c;
                                });
        }

        protected IEnumerable<CityLocation> _distinctCityLocations(List<IEntity> cityLocations)
        {
            return cityLocations.Cast<CityLocation>()
                                .Distinct(new CityLocationComparer())
                                .Select(cl =>
                                {
                                    if (cl.EnCity != null) cl.EnCity = null;
                                    if (cl.EsCity != null) cl.EsCity = null;
                                    if (cl.DeCity != null) cl.DeCity = null;
                                    if (cl.FrCity != null) cl.FrCity = null;
                                    if (cl.RuCity != null) cl.RuCity = null;
                                    if (cl.EnCity != null) cl.EnCity = null;
                                    if (cl.EnCity != null) cl.EnCity = null;
                                    if (cl.EnCity != null) cl.EnCity = null;
                                    return cl;
                                });
        }

        #endregion
    }
}
