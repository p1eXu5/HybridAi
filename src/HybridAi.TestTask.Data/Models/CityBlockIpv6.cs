﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlockIpv6 : CityBlock
    {
        public CityBlockIpv6( string network )
        {
            Network = network;
        }

        [StringLength(39)]
        [Required]
        public string Network { get; }

        public override string ToString()
        {
            return $"{this.Network}";
        }

        public override int GetHashCode()
        {
            return Network.GetHashCode();
        }
    }
}
