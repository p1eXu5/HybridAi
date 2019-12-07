using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class FileLocationRequest : Request
    {
        public FileLocationRequest( string path )
        {
            Path = path;
        }

        public string Path { get; }

        public override IResponse< Request > Response => new Response< FileLocationRequest >( this );
    }
}
