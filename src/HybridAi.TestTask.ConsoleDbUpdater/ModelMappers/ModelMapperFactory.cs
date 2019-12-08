using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ModelMappers
{
    public class ModelMapperFactory
    {
        private static ModelMapperFactory? _instance;

        public static ModelMapperFactory Instance
            => _instance ??= new ModelMapperFactory();


        private Dictionary< string, Type >? _map;

        private Dictionary< string, Type > Map
            => _map ??= new Dictionary< string, Type >( 2 );

        protected ModelMapperFactory() { }

        public IModelMapper< List< IEntity > >? TryFindModelMapper( string header )
        {
            var map = Map;

            if ( map.ContainsKey( header ) ) {
                return ( IModelMapper< List< IEntity > > )Activator.CreateInstance( map[header] );
            }

            return null;
        }

        public void Register< T >( string[] header )
            where T : IModelMapper< List<IEntity>? >
        {
            var map = Map;

            map[String.Join( ',', header )] = typeof( T );
        }
    }
}
