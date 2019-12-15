using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ModelMappers
{
    public abstract class ModelMapper : IModelMapper< List<IEntity>? >
    {
        protected readonly char[] _Splitters = new[] { ',', ';' };

        public abstract string[] Header { get; }

        public List<IEntity>? Result { get; private set; }

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

            var result = new List< IEntity >( new IEntity[lines.Length - offset] );

            Parallel.For( offset, lines.Length, ( i, s ) => {
                IEntity? entity = _map( lines[i] );
                if ( entity != null ) {
                    result[i - offset] = entity;
                }
            } );

            Result = result;
        }

        protected abstract IEntity? _map( string line );

        protected string[] _Split( string[] values )
        {
            bool isOpen = false;
            List< string> resValues = new List< string >();
            int offset = 1;

            for ( int i = 0; i < values.Length; ++i )
            {
                if ( isOpen && i > 0 ) {
                    resValues[ i - offset] += "," + values[ i ];
                    ++offset;
                }
                else {
                    resValues.Add( values[i] );
                }

                int ind = 0;
                do {
                    ind = values[i].IndexOf( '\"', ind );

                    if ( ind >= 0 ) {
                        isOpen = !isOpen;
                        ind = values[i].IndexOf( '\"', ++ind );
                    }

                } while ( ind >= 0 );
            }
            return resValues.ToArray();
        }
    }
}
