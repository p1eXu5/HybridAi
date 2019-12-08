using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ModelMappers
{
    public interface IModelMapper< out TCollection >
        where TCollection : HashSet< IEntity >?
    {
        string[] Header { get; }

        TCollection Result { get; }

        Task BuildModelCollectionAsync( string fileName, int offset = 0 );
    }
}
