using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class UrlRequest : Request
    {
        private string _url;

        public UrlRequest( string url )
        {
            _url = url;
        }

        public override IResponse< Request > Response => new Response< UrlRequest >( this );
    }
}
