using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlockIpv4 : CityBlock
    {
        public CityBlockIpv4( string network )
        {
            Network = network;
        }

        [StringLength(15)]
        [Required]
        public string Network { get; }

        public override string ToString()
        {
            return $"{this.Network}; {base.ToString()}";
        }

        public override int GetHashCode()
        {
            return Network.GetHashCode();
        }
    }
}
