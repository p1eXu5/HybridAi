using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class FileLocationResponse : Response
    {
        public FileLocationResponse( string path )
        {
            Path = path;
        }

        public string Path { get; }
    }
}
