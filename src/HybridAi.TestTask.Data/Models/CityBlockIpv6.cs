using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlockIpv6
    {
        [StringLength(39)]
        public string Network { get; set; }

        public int RegistredCountryGeonameId { get; set; }
        public int RepresentedCountryGeonameId { get; set; }
        public bool IsAnonymousProxy { get; set; }
        public bool IsSatelliteProvider { get; set; }

        [StringLength(8)]
        public string PostalCode { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public short AccuracyRadius { get; set; }
        public int CityLocationGeonameId { get; set; }

        public CityLocation CityLocation { get; set; }
    }
}
