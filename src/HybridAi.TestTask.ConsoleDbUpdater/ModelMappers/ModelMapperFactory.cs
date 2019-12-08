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

        protected ModelMapperFactory() { }

        public IModelMapper< HashSet< IEntity > >? TryFindModelMapper( string header )
        {
            throw new NotImplementedException();
        }
    }
}
