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
        public FolderRequest( IEnumerable< FileInfo > fileInfos )
        {
            FileInfos = fileInfos.ToArray();
        }


        public FileInfo[] FileInfos { get; }

        public override IResponse< Request > Response => new Response< FolderRequest >( this );
    }
}
