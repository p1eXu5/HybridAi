using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlock : IEntity, IEquatable< CityBlock >
    {
        public int? CityLocationGeonameId { get; set; }

        public CityLocation CityLocation { get; set; }



        public int? RegistredCountryGeonameId { get; set; }
        public CityLocation CountryLocation { get; set; }
        
        
        
        public int? RepresentedCountryGeonameId { get; set; }

        public bool IsAnonymousProxy { get; set; }
        public bool IsSatelliteProvider { get; set; }

        [StringLength(8)]
        public string PostalCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public short? AccuracyRadius { get; set; }



        public bool Equals( CityBlock other )
        {
            if ( other == null ) return false;

            return GetNetwork().Equals( other.GetNetwork() );
        }

        public override string ToString()
        {
            return $"GeonameId: {CityLocationGeonameId}";
        }

        public virtual string GetNetwork() => String.Empty;
    }
}
