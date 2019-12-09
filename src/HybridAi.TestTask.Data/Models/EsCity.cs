using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class EsCity : City
    {
#nullable enable
        public EsCity( int geonameId ) : base( geonameId )
        { }
#nullable restore
    }
}
