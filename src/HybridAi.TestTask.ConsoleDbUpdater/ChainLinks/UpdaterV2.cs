﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Comparators;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class UpdaterV2 : Updater
    {
        public UpdaterV2( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }

        protected override IResponse< Request > _process( List< IEntity >[] importedEntities )
        {
            var( blocks, locations) = _decollateEntities( importedEntities );



            return base._process( importedEntities );
        }

        /// <inheritdoc />
        protected override bool _UpdateCityBlocks( List< IEntity >[] importedEntities, out int newCount, out int updCount )
        {
            return base._UpdateCityBlocks( importedEntities, out newCount, out updCount );
        }

        /// <inheritdoc />
        protected override bool _UpdateCityLocations( List< IEntity >[] colls, out int newCount, out int updCount )
        {
            return base._UpdateCityLocations( colls, out newCount, out updCount );
        }


        protected IEnumerable< LocaleCode > _distinctLocaleCodes( List< IEntity > entities )
        {
            return entities.Cast< CityLocation >().SelectMany( e => {
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
    }
}
