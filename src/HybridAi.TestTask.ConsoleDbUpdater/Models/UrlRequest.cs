using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class UrlRequest : Request
    {

        public UrlRequest( string url )
        {
            Url = url;
        }

        public string Url { get; }
        
        public override IResponse< Request > Response => new Response< UrlRequest >( this );
    }
}
