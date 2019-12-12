using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlockIpv4 : CityBlock, IEquatable< CityBlockIpv4 >
    {
        public CityBlockIpv4( string network )
        {
            Network = network;
        }

        [StringLength(15)]
        [Required]
        public string Network { get; }

        public override string GetNetwork() => Network;

        public override string ToString()
        {
            return $"{this.Network}; {base.ToString()}";
        }

        public bool Equals( CityBlockIpv4 other )
        {
            if ( other == null ) return false;

            return Network.Equals( other.Network );
        }

        public override int GetHashCode()
        {
            return Network.GetHashCode();
        }
    }
}
