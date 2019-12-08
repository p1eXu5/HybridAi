using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class FolderRequest : Request
    {
        public FolderRequest( IEnumerable< string > fileInfos )
        {
            Files = fileInfos.ToArray();
        }


        public string[] Files { get; }

        public override IResponse< Request > Response => new Response< FolderRequest >( this );
    }
}
