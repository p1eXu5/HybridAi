using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class UpdaterV2 : Updater
    {
        public UpdaterV2( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }

        protected override IResponse< Request > _process( List< IEntity >[] importedEntities )
        {
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

        private HashSet< LocaleCode > _distinctLocaleCodes( List<IEntity>[] entities )
        {
            throw new NotImplementedException();
        }
    }
}
