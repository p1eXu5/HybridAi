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



        

        [StringLength(4)]
        public string? MetroCode { get; set; }
        
#nullable restore

        [StringLength(128)]
        [Required]
        public string TimeZone { get; set; }

        public bool IsInEuropeanUnion { get; set; }


        public ICollection< CityBlockIpv4 > CityBlockIpv4Collection { get; set; }
        public ICollection< CityBlockIpv6 > CityBlockIpv6Collection { get; set; }

        public RuCity RuCity { get; set; }
        public EnCity EnCity { get; set; }
        public DeCity DeCity { get; set; }
        public FrCity FrCity { get; set; }
        public EsCity EsCity { get; set; }
        public JaCity JaCity { get; set; }
        public PtBrCity PtBrCity { get; set; }
        public ZhCnCity ZhCnCity { get; set; }

        public override int GetHashCode()
        {
            return GeonameId.GetHashCode();
        }
    }
}
