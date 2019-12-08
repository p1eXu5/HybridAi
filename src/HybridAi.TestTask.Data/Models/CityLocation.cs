using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class CityLocation : IEntity
    {
        public CityLocation( int geonameId )
        {
            GeonameId = geonameId;
        }


        public int GeonameId { get; }

        [StringLength(8)]
        [Required]
        public string ContinentCode { get; set; }

        

        [StringLength(8)]
        [Required]
        public string CountryIsoCode { get; set; }

        
#nullable enable

        [StringLength(4)]
        public string? Subdivision1IsoCode { get; set; }

        

        [StringLength(4)]
        public string? Subdivision2IsoCode { get; set; }

        

        [StringLength(4)]
        public string? MetroCode { get; set; }
        
#nullable restore

        [StringLength(128)]
        [Required]
        public string TimeZone { get; set; }

        public bool IsInEuropeanUnion { get; set; }


        public ICollection< CityBlock > CityBlockIpv4Collection { get; set; }
        public ICollection< CityBlockIpv6 > CityBlockIpv6Collection { get; set; }

        public RuCity RuCity { get; set; }
        public EnCity EnCity { get; set; }
        public DeCity DeCity { get; set; }
        public FrCity FrCity { get; set; }
        public EsCity EsCity { get; set; }
        public JaCity JaCity { get; set; }
        public PtBrCity PtBrCity { get; set; }
        public ZhCnCity ZhCnCity { get; set; }
    }
}
