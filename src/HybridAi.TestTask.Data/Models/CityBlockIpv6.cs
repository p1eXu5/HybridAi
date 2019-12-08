using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlockIpv6 : CityBlock
    {
        public CityBlockIpv6( string network ) : base( network )
        { }
    }
}
