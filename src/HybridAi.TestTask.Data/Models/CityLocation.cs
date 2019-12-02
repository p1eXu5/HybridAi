using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityLocation
    {
        public int GeonameId { get; set; }

        [StringLength(8)]
        public string LocaleCode { get; set; }

        [StringLength(8)]
        public string ContinentCode { get; set; }

        [StringLength(64)]
        public string ContinentName { get; set; }

        [StringLength(8)]
        public string CopuntryIsoCode { get; set; }

        [StringLength(128)]
        public string CountryName { get; set; }

        [StringLength(4)]
        public string Subdivision1IsoCode { get; set; }

        [StringLength(128)]
        public string Subdivision1Name { get; set; }

        [StringLength(4)]
        public string Subdivision2IsoCode { get; set; }

        [StringLength(128)]
        public string Subdivision2Name { get; set; }

        [StringLength(4)]
        public string MetroCode { get; set; }

        [StringLength(128)]
        public string TimeZone { get; set; }

        public bool IsInEuropeanUnion { get; set; }

        [StringLength(128)]
        public string CityName { get; set; }

        public ICollection< CityBlockIpv4 > CityBlockIpv4Collection { get; set; }
        public ICollection< CityBlockIpv6 > CityBlockIpv6Collection { get; set; }
    }
}
