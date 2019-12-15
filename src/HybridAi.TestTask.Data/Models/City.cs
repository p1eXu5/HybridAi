using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HybridAi.TestTask.Data.Models
{
    public class City : IEntity
    {
#nullable enable

        public City( int geonameId )
        {
            GeonameId = geonameId;
        }

#nullable restore

        public int GeonameId { get; }

        [StringLength(64, MinimumLength = 1)]
        public string ContinentName { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string CountryName { get; set; }

        [StringLength(128)]
        public string Subdivision1Name { get; set; }

        [StringLength(128)]
        public string Subdivision2Name { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string CityName { get; set; }

        public string LocaleCodeName { get; set; }
        public LocaleCode LocaleCode { get; set; }

        public CityLocation CityLocation { get; set; }

        public override string ToString()
        {
            return $"{GeonameId} ({LocaleCodeName})";
        }

        public override int GetHashCode()
        {
            return GeonameId.GetHashCode();
        }
    }
}
