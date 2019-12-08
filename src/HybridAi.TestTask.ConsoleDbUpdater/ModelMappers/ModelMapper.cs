using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ModelMappers
{
    public abstract class ModelMapper : IModelMapper< HashSet<IEntity>? >
    {
        protected readonly char[] _Splitters = new[] { ',', ';' };

        public abstract string[] Header { get; }

        public HashSet<IEntity>? Result { get; private set; }

        public async Task BuildModelCollectionAsync(string fileName, int offset = 0)
        {
            string[] lines = new string[0];

            try {
                lines = await File.ReadAllLinesAsync( fileName );
            }
            catch ( Exception ex ) {
                LoggerFactory.Instance.Log( ex.Message );
            }

            if ( offset >= lines.Length ) return;

            var result = new HashSet< IEntity >( lines.Length );

            Parallel.For( offset, lines.Length - 1, ( i, s ) => {
                IEntity? entity = _map( lines[i] );
                if ( entity != null ) {
                    result.Add( entity );
                }
            } );

            Result = result;
        }

        protected abstract IEntity? _map( string line );
    }
}
