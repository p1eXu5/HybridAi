using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HybridAi.TestTask.Data.Models
{
    public class CityBlock : IEntity
    {
        public int? RegistredCountryGeonameId { get; set; }
        public int? RepresentedCountryGeonameId { get; set; }
        public bool IsAnonymousProxy { get; set; }
        public bool IsSatelliteProvider { get; set; }

        [StringLength(8)]
        public string PostalCode { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public short AccuracyRadius { get; set; }

        public int CityLocationGeonameId { get; set; }

        public CityLocation CityLocation { get; set; }

        public override string ToString()
        {
            return $"GeonameId: {CityLocationGeonameId}";
        }

        public virtual string GetNetwork() => String.Empty;
    }
}
