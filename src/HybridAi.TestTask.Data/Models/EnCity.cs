using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class EnCity : City
    {
#nullable enable
        public EnCity( int geonameId) : base( geonameId )
        { }
#nullable restore
    }
}
