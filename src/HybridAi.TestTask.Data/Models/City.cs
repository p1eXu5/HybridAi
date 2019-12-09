﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HybridAi.TestTask.Data.Models
{
    public class City : IEntity
    {
        public City( string continentName, string countryName )
        {
            ContinentName = continentName;
            CountryName = countryName;
        }

        [StringLength(64, MinimumLength = 1)]
        [Required]
        public string ContinentName { get; }

        [StringLength(128, MinimumLength = 1)]
        [Required]
        public string CountryName { get; }

        [StringLength(128)]
        public string Subdivision1Name { get; set; }

        [StringLength(128)]
        public string Subdivision2Name { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string CityName { get; set; }


        public Int16 LocaleCodeId { get; set; }

        public LocaleCode LocaleCode { get; set; }

        public ICollection< CityLocation > CityLocations { get; set; }

        public override int GetHashCode()
        {
            return ContinentName.GetHashCode() + 13 * CountryName.GetHashCode() + 27 * CityName.GetHashCode();
        }
    }
}
