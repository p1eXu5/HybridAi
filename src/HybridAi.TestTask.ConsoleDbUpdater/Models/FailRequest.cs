using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class FailRequest : MessageRequest
    {
        public FailRequest( string message ) : base( message )
        {
        }
    }
}
